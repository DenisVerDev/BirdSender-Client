using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessangerClient.Controls;
using MessangerClient.ServiceReference;
using Message = MessangerClient.ServiceReference.Message;

namespace MessangerClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool IsGettingNewMsg = false;
        public MainWindow(User user, bool isnew)
        {
            InitializeComponent();
            App.CurrentUser = user;
            if (isnew) InitializeClientData();
            this.Closed += MainWindow_Closed;
            this.MaxHeight = SystemParameters.WorkArea.Height;
            this.MaxWidth = SystemParameters.WorkArea.Width;
            infobar.SearchForUser += Infobar_SearchForUser;
            infobar.NewActionStarted += Infobar_NewActionStarted;
            infobar.NewActionContext += Infobar_NewActionContext;
            channellist.ValueSelected += Channellist_ValueSelected;
            userentermsg.NewTextMessage += Userentermsg_NewTextMessage;
            userentermsg.NewFileMessage += Userentermsg_NewFileMessage;
            viewer.ScrollChanged += Viewer_ScrollChanged;
            App.ConnectingEvent += App_ConnectingEvent;
            App.ClientResponse.NewMessageCameOut += ClientResponse_NewMessageCameOut;
            App.ConnectToServer();
        }

        private void App_ConnectingEvent(App.ConnectionStatus status)
        {
            this.Dispatcher.Invoke(() => { 
            switch (status)
            {
                case App.ConnectionStatus.Connected:
                        
                        ChannelControl cc = channellist.GetSelectedChannelControl();
                        if (cc != null)
                        {
                            if (!cc.IsGroup) infobar.LoadChannelInfo(cc.ChannelName);
                            else infobar.LoadChannelInfo(cc.Address);
                        }
                        channellist.UpdateChannels();
                        break;

                case App.ConnectionStatus.Connecting:
                    infobar.ChannelConnecting();
                    break;

                case App.ConnectionStatus.ConnectionLost:
                    infobar.ChannelConnectinLost();
                    break;
            }
            });

        }

        private void ClientResponse_NewMessageCameOut(ServiceReference.Message msg)
        {
            Channel channel = channellist.Channels.Where(x => x.Address == msg.Address).FirstOrDefault();
            if(channel == null) //if this chat is new for client
            {
                if(msg.Address.Contains("_")) //if this is chat
                {
                    string otheruser = msg.Address.Substring(0, msg.Address.IndexOf("_"));
                    channel = new Chat(new string[] { otheruser, App.CurrentUser.Username });
                    channellist.Channels.Add(channel);
                }
                else //if this is group
                {
                    string service = msg.Text.Substring(0, msg.Text.IndexOf("]")+1);
                    service = service.Remove(0, service.IndexOf(",")+1);
                    string admin = service.Substring(service.IndexOf("admin=") + 6, service.IndexOf(",") - service.IndexOf("admin=") - 6);
                    string name = service.Substring(service.IndexOf("name=") + 5, service.IndexOf("]") - service.IndexOf("name=") - 5);
                    channel = new Group(admin, name, msg.Address);
                    channellist.Channels.Add(channel);
                }

                bool issearching = false;
                this.Dispatcher.Invoke(() => {
                    if (infobar.tbsearch.Text != String.Empty) issearching = true;
                });
                if(infobar.State == InfoBar.ClickResult.Chats && channel.IsGroup == false && !issearching) this.Dispatcher.Invoke(() => { channellist.AddNewChannelControl(channel); });
                else if(infobar.State == InfoBar.ClickResult.Groups && channel.IsGroup && !issearching)
                {
                    this.Dispatcher.Invoke(() => { channellist.AddNewChannelControl(channel); });
                }
            }

            if (MessageSerializer.IsCorrectServiceMessage(msg.Text))
            {
                if (msg.Text.StartsWith("[service type=ivitation")) infobar.LoadChannelInfo(msg.Address);
                if(msg.Text.StartsWith("[service type=chupdate"))
                {
                    string newname = msg.Text.Substring(msg.Text.IndexOf("groupname=") + 10, msg.Text.IndexOf("]") - msg.Text.IndexOf("groupname=") - 10);
                    this.Dispatcher.Invoke(() => {
                        if (channel.IsGroup)
                        {
                            (channel as Group).ChangeName(newname);
                            ChannelControl cc = channellist.GetChannelControl(channel);
                            if (cc != null) cc.ChannelName = newname;
                            if(channel == channellist.GetSelectedChannel()) infobar.LoadChannelName(newname);
                        }
                    });
                }
                channel.ReceiveMessage(msg);

                this.Dispatcher.Invoke(() =>
                {
                    if (channel == channellist.GetSelectedChannel())
                    {
                        System.Windows.Controls.Control control = MessageSerializer.GetMessageControl(msg, channel);
                        msgcon.Children.Add(control);
                    }
                    else channel.UnreadMessages++;
                    channellist.UpdateChannelControl(msg.Address);
                });
            }
            else
            {
                switch (MessageSerializer.GetServiceMessageType(msg.Text))
                {
                    case MessageSerializer.ServiceType.UpdateUserStatus:
                        this.Dispatcher.Invoke(() => {
                            if (channel == channellist.GetSelectedChannel() && !App.IsConnecting) infobar.LoadChannelInfo(msg.Username);
                        });
                        break;

                       

                    case MessageSerializer.ServiceType.DeleteChannel:
                        this.Dispatcher.Invoke(() => {
                            if (channel == channellist.GetSelectedChannel())
                            {
                                FileControl.StopPlaying();
                                this.channellist.RemoveChannel(channel);
                                Infobar_NewActionStarted(infobar.State);
                            }
                            else
                            {
                                this.channellist.RemoveChannelControl(channel);
                                this.channellist.RemoveChannel(channel);
                            }
                        });
                        break;

                    case MessageSerializer.ServiceType.Kicked:
                        this.Dispatcher.Invoke(() => {
                            string name = MessageSerializer.GetTextValue(msg.Text);
                            if (App.CurrentUser.Username == name)
                            {
                                if (channel == channellist.GetSelectedChannel())
                                {
                                    FileControl.StopPlaying();
                                    this.channellist.RemoveChannel(channel);
                                    Infobar_NewActionStarted(infobar.State);
                                }
                                else
                                {
                                    this.channellist.RemoveChannelControl(channel);
                                    this.channellist.RemoveChannel(channel);
                                }
                            }
                            else if (channel == channellist.GetSelectedChannel() && !App.IsConnecting) infobar.LoadChannelInfo(msg.Address);
                        });
                        break;

                    case MessageSerializer.ServiceType.Stream:
                        this.Dispatcher.Invoke(() => {
                            string address = MessageSerializer.GetTextValue(msg.Text);

                            if (channel == channellist.GetSelectedChannel() && !App.IsConnecting)
                            {
                                infobar.StreamAddress = address;
                                infobar.UpdateStreamStatus();
                            }
                        });
                        break;

                    default: break;
                }

            }

        }

        //-----------------------------------------EVENTS HANDLERS----------------------------------

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                if (infobar.StrState == InfoBar.StreamState.Opened) App.Client.StopCall(App.CurrentUser.Username, infobar.PrevStreamAddress);
                App.Client.Disconnect(App.CurrentUser.Username);
            }
            catch(Exception ex)
            {

            }
        }

        private async void Channellist_ValueSelected(string address, string channelname)
        {
            InfoBar.ClickResult result = infobar.State;

            await Task.Run(() => {
           
                string info = String.Empty;

            switch (result)
            {
                case InfoBar.ClickResult.Chats:

                    this.Dispatcher.Invoke(() => 
                    {
                        infobar.LoadChannelName(channelname);
                        if (!App.IsConnecting) infobar.LoadChannelInfo(channelname);  
                    });
                    break;

                case InfoBar.ClickResult.Groups:
                        this.Dispatcher.Invoke(() => {
                            infobar.LoadChannelName(channelname);
                            if (!App.IsConnecting) infobar.LoadChannelInfo(address);
                        });
                    break;

                default:break;
            }

                Channel channel = null;
                this.Dispatcher.Invoke(() => 
                {
                    msgcon.Children.Clear();
                    userentermsg.Visibility = Visibility.Visible;
                    channel = channellist.Channels.Where(x => x.Address == address).FirstOrDefault();
                    if (channel != null)
                    {
                        infobar.StreamAddress = channel.Address;
                        userentermsg.StopRecording();
                        FileControl.StopPlaying();
                        userentermsg.Address = channel.Address;
                        userentermsg.IsGroup = channel.IsGroup;
                        channel.SaveConfigurations(); 
                        foreach(Message msg in channel.LastMessages)
                        {
                            System.Windows.Controls.Control control = MessageSerializer.GetMessageControl(msg,channel);
                            msgcon.Children.Add(control);
                        }
                        viewer.ScrollToEnd();
                        if (channel.NewMsgCount > 0 && !App.IsConnecting)
                        {
                            viewer.CanContentScroll = false;
                            this.LoadNewMessages(channel);
                        }
                    }
                    else
                    {
                        infobar.StreamAddress = String.Empty;
                        userentermsg.Address = address;
                        if (address.Contains("_")) userentermsg.IsGroup = false;
                        else userentermsg.IsGroup = true;
                    }
                });
            });
        }

        private void Infobar_NewActionStarted(Controls.InfoBar.ClickResult result)
        {
            switch(result)
            {

                case Controls.InfoBar.ClickResult.Chats:
                    channellist.LoadChats();
                    infobar.UnloadChannelInfo();
                    userentermsg.Visibility = Visibility.Hidden;
                    msgcon.Children.Clear();
                    userentermsg.StopRecording();
                    FileControl.StopPlaying();
                    break;

                case Controls.InfoBar.ClickResult.Groups:
                    channellist.LoadGroups();
                    infobar.UnloadChannelInfo();
                    userentermsg.Visibility = Visibility.Hidden;
                    msgcon.Children.Clear();
                    userentermsg.StopRecording();
                    FileControl.StopPlaying();
                    break;
            }
        }

        private void Infobar_NewActionContext(InfoBar.ContextResult cr)
        {
            Channel channel = channellist.GetSelectedChannel();
            switch (cr)
            {
                case InfoBar.ContextResult.DeleteChannel:
                    if (!App.IsConnecting) DeleteChannel();
                    break;

                case InfoBar.ContextResult.LeaveGroup:
                    if (!App.IsConnecting) LeaveGroup(App.CurrentUser.Username);
                    break;

                case InfoBar.ContextResult.KickUsers:
                    KickInviteWindow kw = new KickInviteWindow(channel.Address,true);
                    kw.Show();
                    break;

                case InfoBar.ContextResult.InviteUsers:
                    KickInviteWindow kw1 = new KickInviteWindow(channel.Address, false);
                    kw1.Show();
                    break;

                case InfoBar.ContextResult.ChannelSettings:
                    ChannelSettings cs = new ChannelSettings();
                    cs.Address = channel.Address;
                    cs.ShowDialog();
                    break;
            }
        }

        private async void DeleteChannel()
        {
            Channel channel = channellist.GetSelectedChannel();
            string user = App.CurrentUser.Username;
            await Task.Run(() => { 
                try
                {
                    ResultCodes result =  App.Client.RemoveChannel(user,channel.Address);
                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); });
                }
            });
        }

        private async void LeaveGroup(string username)
        {
            Channel channel = channellist.GetSelectedChannel();
            await Task.Run(() => {
                try
                {
                    ResultCodes result = App.Client.LeaveGroup(username, channel.Address);
                }
                catch (Exception ex)
                {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); });
                }
            });
        }

        private void Infobar_SearchForUser(User user, bool end)
        {
            this.Dispatcher.Invoke(() => 
            {
                if(!end)
                {
                    infobar.UnloadChannelInfo();
                    userentermsg.Visibility = Visibility.Hidden;
                    userentermsg.StopRecording();
                    FileControl.StopPlaying();
                    msgcon.Children.Clear();
                    channellist.ShowSearchUserResult(user);
                }
                else
                {
                    if (infobar.State == InfoBar.ClickResult.Chats) channellist.LoadChats();
                }
            });
        }


        private void Userentermsg_NewFileMessage(string path, DateTime sendTime)
        {
            try
            {
                Channel channel = channellist.GetSelectedChannel(); 
                InfoBar.ClickResult clickres = infobar.State;
                if (channel == null)
                {
                    if (clickres == InfoBar.ClickResult.Chats && App.IsConnecting == false)
                    {
                        channel = channellist.CreateNewChat();
                        CreateChat(new string[] { channel.Users[0], channel.Users[1] });
                    }
                }

                if (path.Contains("/temporary files/"))
                {
                    string mode = "Chats";
                    if (channel.IsGroup) mode = "Groups";
                    string newdest = String.Format("ClientData/{0}/{1}/files/{2}", mode, channel.Address, System.IO.Path.GetFileName(path));
                    File.Copy(path, newdest);
                    File.Delete(path);
                    path = newdest;
                }

                FileControl fc = new FileControl();
                    fc.IsMine = true;
                    fc.SendTime = sendTime;
                    fc.FileInfo = path;
                    fc.Channel = channel;
                    fc.ControlType = FileControl.FileType.Upload;
                    fc.Id = msgcon.Children.Count;
                    fc.SendCancel += Fc_SendCancel;
                

                    if (!App.IsConnecting) fc.UploadFile();

                    msgcon.Children.Add(fc);
                    viewer.ScrollToEnd();

            }
            catch(Exception ex)
            {

            }
        }

        private void Fc_SendCancel(int id)
        {
            msgcon.Children.RemoveAt(id);
        }

        private  void Userentermsg_NewTextMessage(string data, DateTime sendTime)
        {
            Channel channel = channellist.GetSelectedChannel();
            InfoBar.ClickResult clickres = infobar.State;

                try
                {
                
                    if (channel == null) // Creating new channel if we havent it
                    {
                        if (clickres == InfoBar.ClickResult.Chats && App.IsConnecting == false)
                        {
                            channel = channellist.CreateNewChat();
                            CreateChat(new string[] { channel.Users[0], channel.Users[1] });
                        }
                    }

                    Message msg = new Message();
                    msg.Address = channel.Address;
                    msg.Username = App.CurrentUser.Username;
                    msg.SendTime = sendTime.ToString();
                    msg.Text = String.Format("[text]{0}", data);

                    
                    MessageControl mc = new MessageControl();
                    mc.Header = "You";
                    mc.IsMine = true;
                    mc.Text = data;
                    mc.SendTime = sendTime;

                    msgcon.Children.Add(mc);
                    viewer.ScrollToEnd();
                    userentermsg.ClearText();
                    channel.SendMessage(msg);
                }
                catch(Exception ex)
                {
                   App.ConnectToServer();
                }
        }

        private void Viewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if(viewer.VerticalOffset == viewer.ScrollableHeight && viewer.ScrollableHeight > 0)
            {
                Channel channel = channellist.GetSelectedChannel();
                if ( channel != null && channel.NewMsgCount > 0 && !App.IsConnecting && !IsGettingNewMsg) LoadNewMessages(channel);
            }
        }

        private async void LoadNewMessages(Channel channel)
        {
            viewer.CanContentScroll = false;
            await Task.Run(() => { 
                try
                {
                    if (!IsGettingNewMsg) IsGettingNewMsg = true;
                    if (IsGettingNewMsg)
                    {
                        ResultCodes result = App.Client.GetNewMessages(App.CurrentUser.Username, channel.Address);
                        if (result == ResultCodes.InProccess) channel.NewMsgCount -= 20;
                        else if (result == ResultCodes.Success) channel.NewMsgCount = 0;
                    }
                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); });
                }
                this.Dispatcher.Invoke(() => 
                { 
                    viewer.CanContentScroll = true;
                    channellist.UpdateChannelControl(channel.Address);
                });
                IsGettingNewMsg = false;
            });
        }

        //---------------------------------------------------------------------------

        public async void CreateChat(string[] users)
        {
            await Task.Run(() => {
                App.Client.AddChat(users[0], users[1]);
            });
        }

        private void InitializeClientData()
        {
                if(!Directory.Exists("ClientData")) Directory.CreateDirectory("ClientData");
                if (!Directory.Exists("ClientData/Chats")) Directory.CreateDirectory("ClientData/Chats");
                if (!Directory.Exists("ClientData/Groups")) Directory.CreateDirectory("ClientData/Groups");
                if (!Directory.Exists("ClientData/temporary files")) Directory.CreateDirectory("ClientData/temporary files");

                FileStream fs = new FileStream("ClientData/userinfo.txt", FileMode.OpenOrCreate, FileAccess.Write);
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(App.CurrentUser.Username);
                    bw.Write(App.CurrentUser.LastOnline.ToString());
                    bw.Close();
                }
             
        }

      

    }
}
