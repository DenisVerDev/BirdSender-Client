using MessangerClient.MSGService;
using NAudio.Wave;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MessangerClient
{
    /// <summary>
    /// Логика взаимодействия для LiveWindow.xaml
    /// </summary>
    public partial class LiveWindow : Window
    {
        public string Address { get; set; }

        public bool Start { get; set; }

        MemoryStream ms = null;

        AudioClass ac = new AudioClass();

        public LiveWindow(string address)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.WorkArea.Height;
            this.MaxWidth = SystemParameters.WorkArea.Width;
            Address = address;
            ac.StreamAddress = this.Address;
            Start = false;
            App.ClientResponse.NewImageCameOut += ClientResponse_NewImageCameOut;
            this.Closing += LiveWindow_Closing;
            ConnectToStream();
        }

        private void LiveWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!App.IsConnecting) Disconnect();
            ac.Dispose();

        }

        public async void Disconnect()
        {
            await Task.Run(() => {
                try
                {
                    App.Client.DisconnectFromCall(App.CurrentUser.Username, Address);
                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); });
                }
            });
        }

        public async void ConnectToStream()
        {
            await Task.Run(() => {
               ResultCodes res = App.Client.ConnectToCall(App.CurrentUser.Username, Address);
                if (res == ResultCodes.Success)
                {
                    ac.StartRecord();
                    ac.StartListening();
                }
            });
        }

        private  void ClientResponse_NewImageCameOut(byte[] data, bool isend)
        {
            if (Start)
            {
                    if (ms == null) ms = new MemoryStream();

                    if (!isend) ms.Write(data, 0, data.Length);
                    else
                    {
                        ms.Write(data, 0, data.Length);
                        BitmapImage bmp = new BitmapImage();
                        bmp.BeginInit();
                        bmp.CacheOption = BitmapCacheOption.OnLoad;
                        ms.Seek(0, SeekOrigin.Begin);
                        bmp.StreamSource = ms;
                        bmp.EndInit();
                        bmp.Freeze();
                        if (bmp != null)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                ImageBrush ib = new ImageBrush(bmp);
                                ib.Stretch = Stretch.Fill;
                                screen.Fill = ib;
                            });
                        }
                       ms.SetLength(0);
                       ms.Position = 0;
                    }
                    
            }
            if (isend) Start = true;
        }
    }
}
