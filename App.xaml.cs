using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using MessangerClient.LogRegModule;
using MessangerClient.MSGService;
using NAudio.Wave;

namespace MessangerClient
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        public delegate void ConnectionDelegate(ConnectionStatus status);
        public static event ConnectionDelegate ConnectingEvent;

        public enum ConnectionStatus
        {
            Connected = 0,
            Connecting=1,
            ConnectionLost=2
        }

        public static MSGServiceClient Client { get; set; }
        public static ClientResponse ClientResponse { get; set; }
        public static InstanceContext context { get; set; }

        public static User CurrentUser { get; set; }

        public static bool IsConnecting = false;

        public static WaveIn Device { get; set; }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ClientResponse = new ClientResponse();
            context = new InstanceContext(ClientResponse);
            Client = new MSGServiceClient(context);

            Device = new NAudio.Wave.WaveIn();
            Device.WaveFormat = new WaveFormat(44001, 1);
            
            if (Directory.Exists("ClientData") && File.Exists("ClientData/userinfo.txt"))
            {
                CurrentUser = GetCurrentUser("ClientData/userinfo.txt");
                MainWindow mw = new MainWindow(CurrentUser, false);
                mw.Show();
            }
            else
            {
                LogRegWindow loginWindow = new LogRegWindow();
                loginWindow.Show();
            }
        }

        public static async void ConnectToServer()
        {
            if (!IsConnecting) IsConnecting = true;
            if (IsConnecting)
            {
                await Task.Run(() =>
                {
                    if (ConnectingEvent != null) ConnectingEvent(ConnectionStatus.Connecting);

                    try
                    {
                        Client = new MSGServiceClient(context);
                        Client.SubscribeToUser(CurrentUser.Username);
                        if (ConnectingEvent != null) ConnectingEvent(ConnectionStatus.Connected);
                    }
                    catch (Exception ex)
                    {
                        if (ConnectingEvent != null) ConnectingEvent(ConnectionStatus.ConnectionLost);
                    }
                    finally
                    {
                        IsConnecting = false;
                    }
                });
            }
        }


        public static User GetCurrentUser(string path)
        {
            User user = new User();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (BinaryReader br = new BinaryReader(fs))
            {
                user.Username = br.ReadString();
                user.LastOnline = Convert.ToDateTime(br.ReadString());
                br.Close();
            }
            return user;
        }
    }
}
