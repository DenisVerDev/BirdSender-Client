using MessangerClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing;

namespace MessangerClient.Controls
{
    /// <summary>
    /// Логика взаимодействия для ChannelList.xaml
    /// </summary>
    public partial class ChannelList : UserControl
    {
        public List<Channel> Channels { get; set; }
        private int SelectedId { get; set; }

        public delegate void ValueDelegate(string address, string channelname);
        public event ValueDelegate ValueSelected;

        public bool MultiSelect { get; set; }

        private List<int> SelectedIndeces { get; set; }

        public bool IsChatsSection { get; set; }

        public static bool IsAdmin {get;set;}

        public ChannelList()
        {
            InitializeComponent();
            Channels = new List<Channel>();
            SelectedIndeces = new List<int>();
            SelectedId = -1;
            LoadChannels();
            MultiSelect = false;
        }

        private void scrollviewer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (scrollviewer.VerticalScrollBarVisibility == ScrollBarVisibility.Auto) scrollviewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            else if (scrollviewer.VerticalScrollBarVisibility == ScrollBarVisibility.Hidden) scrollviewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        public void LoadChats()
        {
            IsChatsSection = true;
            list.Children.Clear();
                SelectedId = -1;
                SelectedIndeces.Clear();
                for (int i = 0; i < Channels.Count; i++)
                {
                    if (Channels[i].IsGroup == false)
                    {
                        AddNewChannelControl(Channels[i]);
                    }
                }
        }

        public void LoadGroups()
        {
            IsChatsSection = false;
            list.Children.Clear();
            SelectedId = -1;
            SelectedIndeces.Clear();
            for (int i = 0; i < Channels.Count; i++)
            {
                if (Channels[i].IsGroup == true)
                {
                    AddNewChannelControl(Channels[i]);
                }
            }
        }

        public void LoadChannels()
        {
            try
            {
                
                //chats load
                string[] chats = Directory.GetDirectories("ClientData/Chats/");
                foreach (string chat in chats)
                {
                    Chat ch = new Chat(Path.GetFileName(chat));
                    Channels.Add(ch);
                }

                string[] groups = Directory.GetDirectories("ClientData/Groups/");
                foreach (string group in groups)
                {
                    Group ch = new Group(Path.GetFileName(group));
                    Channels.Add(ch);
                }
            }
            catch (Exception ex)
            {

            }
            LoadChats();
        }

        private void Cc_ChannelSelected(int id)
        {
            try
            {
                if (!MultiSelect)
                {
                    if (SelectedId != -1) (list.Children[SelectedId] as ChannelControl).IsSelected = false;
                }

                SelectedId = id;
                if (!MultiSelect) (list.Children[SelectedId] as ChannelControl).IsSelected = true;
                else
                {
                    bool status = (list.Children[SelectedId] as ChannelControl).IsSelected;
                    if (!status) SelectedIndeces.Add(SelectedId);
                    else SelectedIndeces.Remove(SelectedId);
                    (list.Children[SelectedId] as ChannelControl).IsSelected = !status;

                }

                if(ValueSelected != null && MultiSelect == false)
                {
                    ChannelControl cc = (list.Children[SelectedId] as ChannelControl);
                    Channel channel = Channels.Where(x => x.Address == cc.Address).FirstOrDefault();
                    if (ValueSelected != null)
                    {
                        if (channel != null)
                        {
                            channel.UnreadMessages = 0;
                            this.UpdateChannelControl(channel.Address);
                            if (channel.IsGroup && (channel as Group).AdminName == App.CurrentUser.Username) IsAdmin = true;
                            else IsAdmin = false;
                        }
                        ValueSelected(cc.Address, cc.ChannelName);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }


        public void ShowSearchUserResult(User user)
        {
            list.Children.Clear();
            if (user != null && user.Username != App.CurrentUser.Username)
            {
                ChannelControl cc = new ChannelControl() { 
                   Id = 0,
                   IsGroup = false,
                   ChannelName = user.Username,
                   Address = App.CurrentUser.Username+"_"+user.Username
                };
                list.Children.Add(cc);
                cc.ChannelSelected += Cc_ChannelSelected;
                if (!App.IsConnecting) LoadUserImage(cc.ChannelName);
            }
        }

        public void ShowSearchUsersResult(User user)
        {
            SelectedId = -1;
            SelectedIndeces.Clear();
            if (user != null && user.Username != App.CurrentUser.Username)
            {
                ChannelControl cc = new ChannelControl()
                {
                    Id = list.Children.Count,
                    IsGroup = false,
                    ChannelName = user.Username,
                    Address = App.CurrentUser.Username + "_" + user.Username
                };
                list.Children.Add(cc);
                cc.ChannelSelected += Cc_ChannelSelected;
                if (!App.IsConnecting) LoadUserImage(cc.ChannelName);
            }
        }

        public async void LoadUserImage(string name)
        {
            await Task.Run(() => {
                try
                {
                    BitmapImage bmp;
                    long pos = 0;
                    byte[] data = null;
                    using(MemoryStream ms = new MemoryStream())
                    {
                        while(true)
                        {
                            ResultCodes result = App.Client.GetUserAvatar(name, pos, out data);
                            if (data != null) ms.Write(data, 0, data.Length);
                            else break;
                            pos += data.Length;
                            if (result == ResultCodes.Success) break;
                        }
                    bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    ms.Seek(0, SeekOrigin.Begin);
                    bmp.StreamSource = ms;
                    bmp.EndInit();
                    bmp.Freeze();
                    }

                    if (bmp != null) 
                    {
                        this.Dispatcher.Invoke(() => {
                            ImageBrush ib = new ImageBrush(bmp);
                            ib.Stretch = Stretch.UniformToFill;
                            ChannelControl cc = list.Children.Cast<ChannelControl>().Where(x => x.ChannelName == name).FirstOrDefault();
                            if(cc != null) cc.avatar.Fill = ib;
                        });
                    }
                }
                catch(Exception ex)
                {
                   
                }
            });
        }

        public async void LoadGroupImage(string address)
        {
            await Task.Run(() => {
                try
                {
                    BitmapImage bmp;
                    long pos = 0;
                    byte[] data = null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        while (true)
                        {
                            ResultCodes result = App.Client.GetGroupAvatar(address, pos, out data);
                            if (data != null) ms.Write(data, 0, data.Length);
                            else break;
                            pos += data.Length;
                            if (result == ResultCodes.Success) break;
                        }
                        bmp = new BitmapImage();
                        bmp.BeginInit();
                        bmp.CacheOption = BitmapCacheOption.OnLoad;
                        ms.Seek(0, SeekOrigin.Begin);
                        bmp.StreamSource = ms;
                        bmp.EndInit();
                        bmp.Freeze();
                    }

                    if (bmp != null)
                    {
                        this.Dispatcher.Invoke(() => {
                            ImageBrush ib = new ImageBrush(bmp);
                            ib.Stretch = Stretch.UniformToFill;
                            ChannelControl cc = list.Children.Cast<ChannelControl>().Where(x => x.Address == address).FirstOrDefault();
                            cc.avatar.Fill = ib;
                        });
                    }
                }
                catch (Exception ex)
                {

                }
            });
        }

        public void UpdateChannelControl(string address)
        {
            ChannelControl cc = list.Children.Cast<ChannelControl>().Where(x => x.Address == address).FirstOrDefault();
            if(cc != null)
            {
                Channel channel = Channels.Where(x => x.Address == address).FirstOrDefault();
                if(channel != null)
                {
                    string text = String.Empty;
                    if (channel.LastMessages.Count > 0) text = channel.LastMessages.Last().Text;
                    text = Path.GetFileName(MessageSerializer.GetTextValue(text));
                    if (text.EndsWith(".wav")) text = "audio";
                    cc.LastMsg = text;
                    cc.NewMessagesCount = channel.NewMsgCount + channel.UnreadMessages;
                }
            }
        }

        public Channel GetSelectedChannel()
        {
            if (SelectedId >= 0)
            {
                string address = (list.Children[SelectedId] as ChannelControl).Address;
                return Channels.Where(x => x.Address == address).FirstOrDefault();
            }
            else return null;
        }

        public ChannelControl GetSelectedChannelControl()
        {
            if(SelectedId >= 0 && !MultiSelect)
            {
                return list.Children[SelectedId] as ChannelControl;
            }
            return null;
        }

        public Chat CreateNewChat()
        {
            ChannelControl cc = list.Children[SelectedId] as ChannelControl;
            Chat chat = new Chat(new string[] { App.CurrentUser.Username, cc.ChannelName });
            Channels.Add(chat);
            return chat;
        }
        

        public void AddNewChannelControl(Channel channel)
        {
            string chname = String.Empty;
            if (channel.IsGroup) chname = (channel as Group).GroupName;
            else
            {
                int indx = channel.Address.IndexOf(App.CurrentUser.Username);
                if (indx == 0)
                {
                    chname = channel.Address.Remove(indx, App.CurrentUser.Username.Length + 1);
                }
                else chname = channel.Address.Remove(indx - 1, App.CurrentUser.Username.Length + 1);
            }

            string lastmsg = String.Empty;
            if (channel.LastMessages.Count > 0)
            {
                lastmsg = Path.GetFileName(MessageSerializer.GetTextValue(channel.LastMessages.Last().Text));
                if (lastmsg.EndsWith(".wav")) lastmsg = "audio";
            }
            else lastmsg = String.Empty;

            ChannelControl cc = new ChannelControl()
            {
                Id = list.Children.Count,
                IsGroup = channel.IsGroup,
                Address = channel.Address,
                ChannelName = chname,
                LastMsg = lastmsg,
                NewMessagesCount = channel.NewMsgCount + channel.UnreadMessages
            };
            list.Children.Add(cc);
            cc.ChannelSelected += Cc_ChannelSelected;
            
            if (!App.IsConnecting && !channel.IsGroup) LoadUserImage(cc.ChannelName);
            else if(!App.IsConnecting) LoadGroupImage(channel.Address);
        }

        public async void UpdateChannels()
        {
            await Task.Run(() => { 
                try
                {
                    //getting new channels
                    string[] newaddresses = null;
                        if (!App.IsConnecting) App.Client.GetInvites(App.CurrentUser.Username, out newaddresses);
                        if(newaddresses != null)
                        {
                            foreach(string address in newaddresses)
                            {
                                Channel channel = null;
                                if(address.Contains("_"))
                                {
                                    string firstuser = address.Substring(0, address.IndexOf("_"));
                                    string seconduser = address.Substring(address.IndexOf("_") + 1);
                                    channel = new Chat(new string[] { firstuser, seconduser });
                                    if (IsChatsSection) this.Dispatcher.Invoke(() => { this.AddNewChannelControl(channel); });
                                    Channels.Add(channel);
                                }
                                else
                                {
                                     string admin = String.Empty;
                                     string name = String.Empty;
                                     if (!App.IsConnecting)
                                     {
                                       ResultCodes result =  App.Client.GetGroupInfo(address, out admin, out name);
                                       if(result == ResultCodes.Success)
                                       {
                                          channel = new Group(admin, name, address);
                                          if (!IsChatsSection) this.Dispatcher.Invoke(() => { this.AddNewChannelControl(channel); });
                                          Channels.Add(channel);
                                       }
                                     }
                                }
                            }
                        }
                    //getting total new messages
                    List<int> removeaddr = new List<int>();
                    for(int i = 0; i < Channels.Count; i++)
                    {
                        long total = 0;
                        ResultCodes result = ResultCodes.ServerError;
                        if (!App.IsConnecting) result = App.Client.GetTotalNewMessages(App.CurrentUser.Username, Channels[i].Address, out total);
                        Channels[i].NewMsgCount = Convert.ToInt32(total);
                        this.Dispatcher.Invoke(() => { UpdateChannelControl(Channels[i].Address); });
                        if (result == ResultCodes.NoMatching)
                        {
                            removeaddr.Add(i);
                        }
                    }
                    foreach(int index in removeaddr)
                    {
                        Channel channel = Channels[index];
                        this.Dispatcher.Invoke(() => { 
                        this.RemoveChannelControl(channel);
                        this.RemoveChannel(channel);
                        });
                    }
                    
                }
                catch(Exception ex)
                {
                    if (!App.IsConnecting) App.ConnectToServer();
                }
            });
        }

        public void HideInfo()
        {
            foreach(ChannelControl cc in list.Children)
            {
                cc.NewMessagesCount = 0;
                cc.LastMsg = String.Empty;
            }
        }


        public string[] GetSelectedUsers()
        {
            string[] users = new string[SelectedIndeces.Count];
            for(int i = 0; i < SelectedIndeces.Count; i++)
            {
                users[i] = (list.Children[SelectedIndeces[i]] as ChannelControl).ChannelName;
            }
            return users;
        }

        public void RemoveChannel(Channel channel)
        {
            if (Channels.Contains(channel))
            {
                channel.RemoveAllData();
                Channels.Remove(channel);
            }
        }

        public void RemoveChannelControl(Channel channel)
        {
            if (Channels.Contains(channel))
            {
                ChannelControl cc = list.Children.Cast<ChannelControl>().Where(x => x.Address == channel.Address).FirstOrDefault();
                if(cc != null) list.Children.Remove(cc);
            }
        }

        public ChannelControl GetChannelControl(Channel channel)
        {
            return list.Children.Cast<ChannelControl>().Where(x => x.Address == channel.Address).FirstOrDefault();
        }
    }
}
