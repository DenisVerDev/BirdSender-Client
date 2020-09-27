using MessangerClient.MSGService;
using NAudio.Wave;
using SharpVectors.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MessangerClient.Controls
{
    /// <summary>
    /// Логика взаимодействия для InfoBar.xaml
    /// </summary>
    public partial class InfoBar : UserControl
    {
        public enum ClickResult 
        { 
            Chats = 1,
            Groups = 2
        }

        public enum ContextResult
        { 
            DeleteChannel = 0,
            ChannelSettings = 1,
            KickUsers = 2,
            InviteUsers=3,
            LeaveGroup =4
        }

        public enum StreamState
        {
            Closed = 0,
            Opened = 1,
            AlreadyStreaming=2,
            None = 3
        }

        public StreamState StrState { get; set; }

        public string PrevStreamAddress { get; set; }

        public string StreamAddress { get; set; }

        public ClickResult State { get; set; }

        public delegate void ActionStart(ClickResult result);
        public event ActionStart NewActionStarted;

        public delegate void SearchUserDelegate(User user,bool end);
        public event SearchUserDelegate SearchForUser;

        public delegate void ActionContext(ContextResult cr);
        public event ActionContext NewActionContext;

        AudioClass ac = new AudioClass();

        public InfoBar()
        {
            InitializeComponent();
            State = ClickResult.Chats;
            StrState = StreamState.Closed;
            tbsearch.Tag = true;
            PrevStreamAddress = String.Empty;
        }

        private void BtnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            State = (ClickResult)Convert.ToInt32(btn.Tag);

            tbsearch.Tag = false;
            tbsearch.Text = String.Empty;
            tbsearch.Tag = true;

            if (NewActionStarted != null)
            {
                NewActionStarted(State);
            }
            
        }

        public void LoadChannelName(string name)
        {
                lbname.Content = name;
                lbstatus.Content = String.Empty;
                channelinfo.Visibility = Visibility.Visible;
                btnsettings.Visibility = Visibility.Visible;
                btnstartcall.Visibility = Visibility.Visible;
        }

        public async void LoadChannelInfo(string channelnameoraddress)
        {
            await Task.Run(() => { 
                try
                {
                    switch (State)
                    {
                        case ClickResult.Chats:
                            Nullable<DateTime> dt;
                            bool isonline;
                            this.Dispatcher.Invoke(() => { ChannelConnecting(); });
                            ResultCodes res = App.Client.GetUserOnlineStatus(channelnameoraddress, out isonline, out dt);
                            this.Dispatcher.Invoke(() => 
                            {
                                if (isonline) lbstatus.Content = "online";
                                else if (dt != null) lbstatus.Content = "Last seen " + (dt as DateTime?).GetValueOrDefault().ToString("MMM dd H:mm", CultureInfo.CreateSpecificCulture("en"));
                                else lbstatus.Content = "Server error";
                            });
                            break;

                        case ClickResult.Groups:
                            this.Dispatcher.Invoke(() => { ChannelConnecting(); });
                            int count = 0;
                            ResultCodes res1 = App.Client.GetGroupUsersCount(channelnameoraddress, out count);
                            this.Dispatcher.Invoke(() =>
                            {
                                if (count > 0) lbstatus.Content = String.Format("{0} users",count);
                                else lbstatus.Content = "Server error";
                            });
                            break;
                    }

                    if(!App.IsConnecting) UpdateStreamStatus();


                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        App.ConnectToServer();
                    });
                }
            
            });
        }

        public void ChannelConnectinLost()
        {
            lbstatus.Content = "Connection lost";
        }

        public void ChannelConnecting()
        {
            lbstatus.Content = "Connecting...";
        }

        public void ChangeState(string info)
        {
            lbstatus.Content = info;
        }

        public void UnloadChannelInfo()
        {
            channelinfo.Visibility = Visibility.Hidden;
            btnsettings.Visibility = Visibility.Collapsed;
            if(StrState != StreamState.Opened) btnstartcall.Visibility = Visibility.Collapsed;
        }

        private async void tbsearch_TextValueChanged(object sender, EventArgs e)
        {
            if ((bool)tbsearch.Tag == true)
            {
                string Name = tbsearch.Text;
                if (tbsearch.Text != String.Empty)
                {
                    await Task.Run(() =>
                    {
                        try
                        {
                            switch (State)
                            {
                                case InfoBar.ClickResult.Chats:
                                    User user = null;
                                    if(!App.IsConnecting)App.Client.SearchUserByUserName(Name, out user);
                                    if (SearchForUser != null) SearchForUser(user, false);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            if (SearchForUser != null) SearchForUser(null, false);
                        }
                    });

                }
                else if (SearchForUser != null) SearchForUser(null, true);
            }
        }

        private void btnappset_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.ShowDialog();
        }

        private void btncreategroup_Click(object sender, RoutedEventArgs e)
        {
            GroupCreator groupCreator = new GroupCreator();
            groupCreator.ShowDialog();
        }

        private void btnsettings_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm;
            switch (State)
            {
                case ClickResult.Chats:
                    cm = FindResource("cmchat") as ContextMenu;
                    cm.PlacementTarget = sender as Button;
                    cm.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                    foreach(MenuItem i in cm.Items)
                    {
                        i.Click += Menu_Click;
                    }
                    cm.IsOpen = true;
                    cm.Closed += Cm_Closed;
                    break;

                case ClickResult.Groups:
                    if(ChannelList.IsAdmin) cm = FindResource("cmadmin") as ContextMenu;
                    else cm = FindResource("cmuser") as ContextMenu;
                    cm.PlacementTarget = sender as Button;
                    cm.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                    foreach (MenuItem i in cm.Items)
                    {
                        i.Click += Menu_Click;
                    }
                    cm.IsOpen = true;
                    cm.Closed += Cm_Closed;
                    break;
            }
        }

        private void Cm_Closed(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = sender as ContextMenu;
            foreach (MenuItem i in cm.Items)
            {
                i.Click -= Menu_Click;
            }
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem i = sender as MenuItem;
            int tag = Convert.ToInt32(i.Tag);
            ContextResult cr = (ContextResult)tag;

            if (NewActionContext != null) NewActionContext(cr);

        }

        public async void UpdateStreamStatus()
        {
            await Task.Run(() => {
                try
                {
                    bool isstreaming;
                    if (!App.IsConnecting)
                    {
                        if (PrevStreamAddress == string.Empty) PrevStreamAddress = StreamAddress;
                        ResultCodes res = App.Client.GetCall(StreamAddress, out isstreaming);
                        if (res == ResultCodes.Success)
                        {
                            if (isstreaming && StrState == StreamState.Closed)
                            {
                                this.Dispatcher.Invoke(() => { (btnstartcall.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnstreamenter.svg"); });
                                StrState = StreamState.AlreadyStreaming;
                            }
                            else if (!isstreaming)
                            {
                                if (PrevStreamAddress == StreamAddress)
                                {
                                    StrState = StreamState.Closed;
                                    this.Dispatcher.Invoke(() => {
                                        (btnstartcall.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btndemonstration.svg");
                                        btnsettings.IsEnabled = true;
                                    });
                                }
                            }
                        }
                        else StrState = StreamState.None;

                    }
                }
                catch (Exception ex)
                {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); });
                }
            });
        }

        private void btnstartcall_Click(object sender, RoutedEventArgs e)
        {
            switch (StrState)
            {
                case StreamState.Closed:
                    btnsettings.IsEnabled = false;
                    StrState = StreamState.Opened;
                    StreamingStart();
                    (btnstartcall.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnstreamcancel.svg");
                    break;

                case StreamState.Opened:
                    StrState = StreamState.Closed;
                    StreamingStop();
                    (btnstartcall.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btndemonstration.svg");
                    btnsettings.IsEnabled = true;
                    break;

                case StreamState.AlreadyStreaming:
                    btnsettings.IsEnabled = false;
                    LiveWindow lw = new LiveWindow(StreamAddress);
                    lw.Show();
                    break;
            }

        }

        private async void StreamingStart()
        {
            await Task.Run(() => { 
                try
                {
                    ResultCodes result =   App.Client.StartCall(App.CurrentUser.Username, StreamAddress);
                    if(result == ResultCodes.Success)
                    {
                        this.Dispatcher.Invoke(() => 
                        {
                            PrevStreamAddress = StreamAddress;
                            StreamImage();
                            ac.StreamAddress = this.StreamAddress;
                            ac.StartRecord();
                            ac.StartListening();
                        });
                    }
                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); StrState = StreamState.Opened; });
                }
            });

        }

        private async void StreamingStop()
        {
            await Task.Run(() => {
                try
                {
                    ResultCodes result = App.Client.StopCall(App.CurrentUser.Username, PrevStreamAddress);
                    ac.StopRecord();
                    ac.StopListening();
                    PrevStreamAddress = StreamAddress;
                }
                catch (Exception ex)
                {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); StrState = StreamState.Opened; });
                }
            });

        }

        private async void StreamImage()
        {
            await Task.Run(() => {
                try
                {
                        ResultCodes res = ResultCodes.Success;
                        System.Drawing.Size size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
                        System.Drawing.Image img = new System.Drawing.Bitmap(size.Width,size.Height);
                        using (Graphics g = Graphics.FromImage(img))
                        {
                           using(MemoryStream ms = new MemoryStream())
                           {
                             g.CopyFromScreen(0, 0, 0, 0, size);
                             img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                             ms.Seek(0, SeekOrigin.Begin);
                             while(ms.Position < ms.Length)
                             {
                                byte[] data = new byte[65000];
                                ms.Read(data, 0, data.Length);
                                bool isend = false;
                                if (ms.Position >= ms.Length) isend = true;
                                if (!App.IsConnecting)
                                {
                                    res = App.Client.SendImages(StreamAddress, data, isend, App.CurrentUser.Username);
                                    if (res != ResultCodes.Success) break;
                                }
                                else break;
                             }
                            ms.Close();
                           }
                        }
                    this.Dispatcher.Invoke(() => {
                        if (res == ResultCodes.Success && StrState == StreamState.Opened) StreamImage();
                    });
                }
                catch (Exception ex)
                {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); StrState = StreamState.Closed; });
                }
            });
        }

     
    }
}
