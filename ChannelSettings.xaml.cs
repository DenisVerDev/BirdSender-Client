using MessangerClient.MSGService;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MessangerClient
{
    /// <summary>
    /// Логика взаимодействия для ChannelSettings.xaml
    /// </summary>
    public partial class ChannelSettings : Window
    {
        public string Address { get; set; }

        private string Path { get; set; }

        private bool IsImageLoaded { get; set; }

        public ChannelSettings()
        {
            InitializeComponent();
        }

        private void btnphoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "png|*.png|jpg|*.jpg";
            if (opf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo fi = new FileInfo(opf.FileName);
                if (fi.Length < 307200) //3kb max
                {
                    Path = opf.FileName;
                    btnphoto.Content = null;
                    ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(opf.FileName, UriKind.Relative)));
                    ib.Stretch = Stretch.Fill;
                    (btnphoto.Template.FindName("body", btnphoto) as Border).Background = ib;
                    IsImageLoaded = true;
                }
                else System.Windows.Forms.MessageBox.Show("You need to choose file with less size", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UploadPhoto(string path, string address)
        {
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
                        result = App.Client.UploadGroupAvatar(address, data, isnew);
                        if (result != ResultCodes.Success) break;
                        isnew = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Dispatcher.Invoke(() => { App.ConnectToServer(); });
            }
        }

        private async void btnapply_Click(object sender, RoutedEventArgs e)
        {
            string text = tbname.Text;
            btnapply.IsEnabled = false;
            await Task.Run(() => {
                try
                {
                    if (IsImageLoaded && !App.IsConnecting) UploadPhoto(Path, Address);
                    if (text != String.Empty && !App.IsConnecting) App.Client.RenameGroup(Address, text);
                    this.Dispatcher.Invoke(() => { this.Close(); });
                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); });
                }
            });
        }
    }
}
