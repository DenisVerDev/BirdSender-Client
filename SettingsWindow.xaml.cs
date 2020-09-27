using MessangerClient.MSGService;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MessangerClient
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {


        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public List<string> InputDevices
        {
            get
            {
                List<string> list = new List<string>();
                for (int i = 0; i < WaveIn.DeviceCount; i++)
                {
                    WaveInCapabilities w = WaveIn.GetCapabilities(i);
                    list.Add(w.ProductName);
                }
                return list;
            }

        }

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void btnavatar_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "png|*.png|jpg|*.jpg";
            if (opf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo fi = new FileInfo(opf.FileName);
                if (fi.Length < 307200) //3kb max
                {
                    btnavatar.IsEnabled = false;
                    if (!App.IsConnecting) UploadPhoto(opf.FileName);
                    else btnavatar.IsEnabled = true;
                }
                else System.Windows.Forms.MessageBox.Show("You need to choose file with less size","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void inputdevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Device.DeviceNumber = inputdevices.SelectedIndex;
        }


        private async void UploadPhoto(string path)
        {
            await Task.Run(() => {
                try
                {
                    ResultCodes result = ResultCodes.InProccess;
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        bool isnew = true;
                        while (fs.Position < fs.Length)
                        {
                            byte[] data = new byte[65000];
                            fs.Read(data, 0, data.Length);
                            result = App.Client.UploadUserAvatar(App.CurrentUser.Username, data, isnew);
                            if (result != ResultCodes.Success) break;
                            isnew = false;
                        }
                    }
                    if (result == ResultCodes.Success)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            btnavatar.Content = null;
                            ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(path, UriKind.Relative)));
                            ib.Stretch = Stretch.Fill;
                            (btnavatar.Template.FindName("body", btnavatar) as Border).Background = ib;
                            btnavatar.IsEnabled = true;
                        });
                    }
                }
                catch (Exception ex)
                {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); });
                }
            });
        }


        // -------------------------------USERNAME PROPERTY---------------------------------
        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(SettingsWindow), new PropertyMetadata(App.CurrentUser.Username, new PropertyChangedCallback(UserNameChanged)));

        private static void UserNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SettingsWindow sw = d as SettingsWindow;
            sw.SetUserName((String)e.NewValue);
        }

        private void SetUserName(string val)
        {
            this.lbusername.Content = val;
        }
    }
}
