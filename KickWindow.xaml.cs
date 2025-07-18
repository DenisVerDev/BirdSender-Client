using MessangerClient.Controls;
using MessangerClient.ServiceReference;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MessangerClient
{
    /// <summary>
    /// Логика взаимодействия для KickInviteWindow.xaml
    /// </summary>
    public partial class KickInviteWindow : Window
    {
        public string Address { get; set; }



        public bool IsKick
        {
            get;
            set;
        }

        public KickInviteWindow(string Address, bool iskick)
        {
            InitializeComponent();
            this.Address = Address;
            this.IsKick = iskick;
            if (!this.IsKick)
            {
                btnkick.Content = "Invite users";
                topbar.TopBarHeader = "Invite users";
            }
            InitializeUsers();
        }

        public async void InitializeUsers()
        {
            if(IsKick) userslist.list.Children.Clear();
            string lastname = String.Empty;
            int count;
            List<string> users = new List<string>(); ;
            ResultCodes result = ResultCodes.InProccess;
            await Task.Run(() => { 
            try
            {
                while (result == ResultCodes.InProccess)
                {
                    if (!App.IsConnecting)
                    {
                        string[] u = null;
                        result = App.Client.GetGroupUsers(Address, lastname, out count, out u);
                        users.AddRange(u);
                        if(IsKick)
                        { 
                           foreach (string username in users)
                           {
                                User user = new User();
                                user.Username = username;
                                this.Dispatcher.Invoke(() => { userslist.ShowSearchUsersResult(user); });
                           }
                        }
                    }
                }
                if(!IsKick)
                {
                        this.Dispatcher.Invoke(() => { 
                        
                        List<string> loadedchannels = userslist.list.Children.Cast<ChannelControl>().Where(x=>x.IsGroup == false).Select(x=>x.ChannelName).ToList();
                        userslist.list.Children.Clear();
                        for(int i = 0; i < users.Count; i++)
                        {
                            if (loadedchannels.Contains(users[i])) loadedchannels.Remove(users[i]);
                        }
                        for(int i = 0; i < loadedchannels.Count; i++)
                        {
                                User user = new User() { Username = loadedchannels[i] };
                                userslist.ShowSearchUsersResult(user);
                        }
                        });
                }
            }
            catch(Exception ex)
            {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); });
            }
            });
        }

        private async void btnkick_Click(object sender, RoutedEventArgs e)
        {
            string[] users = userslist.GetSelectedUsers();
            string address = Address;
            btnkick.IsEnabled = false;
            await Task.Run(() => { 
                try
                {
                    foreach(string user in users)
                    {
                        if (!App.IsConnecting && IsKick) App.Client.LeaveGroup(user, address);
                        else if (!App.IsConnecting) App.Client.InviteToGroup(address, user);
                    }
                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); });
                }
                finally
                {
                    this.Dispatcher.Invoke(() => { this.Close(); });
                }
            });
        }
    }
}
