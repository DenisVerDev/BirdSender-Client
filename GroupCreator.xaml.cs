using MessangerClient.ServiceReference;
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
    /// Логика взаимодействия для GroupCreator.xaml
    /// </summary>
    public partial class GroupCreator : Window
    {

        private string Path { get; set; }

        private bool IsImageLoaded { get; set; }

        public GroupCreator()
        {
            InitializeComponent();
            userslist.HideInfo();
            IsImageLoaded = false;
        }

        private void btncreate_Click(object sender, RoutedEventArgs e)
        {
            if(tbname.Text.Length > 7)
            {
                if (!App.IsConnecting) CreateGroup();
            }
            else System.Windows.Forms.MessageBox.Show("Group name must have more than 7 symbols", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnavatar_Click(object sender, RoutedEventArgs e) // preview
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "png|*.png|jpg|*.jpg";
            if (opf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo fi = new FileInfo(opf.FileName);
                if (fi.Length < 307200) //3kb max
                {
                    Path = opf.FileName;
                    btnavatar.Content = null;
                    ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(opf.FileName, UriKind.Relative)));
                    ib.Stretch = Stretch.Fill;
                    (btnavatar.Template.FindName("body", btnavatar) as Border).Background = ib;
                    IsImageLoaded = true;
                }
                else System.Windows.Forms.MessageBox.Show("You need to choose file with less size", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void CreateGroup()
        {
            string GroupName = tbname.Text;
            string path = Path;
            string[] users = userslist.GetSelectedUsers();
            await Task.Run(() => { 
                try
                {
                    DateTime time = DateTime.Now;
                    string address = String.Format("{0}.{1}.{2}.group", time.ToShortDateString(), time.ToLongTimeString(), time.Millisecond);
                    address = address.Replace(":", ".");
                    ResultCodes result = App.Client.AddGroup(GroupName, App.CurrentUser.Username, address);
                    if (result == ResultCodes.Success)
                    {
                        foreach(string user in users)
                        {
                            if (!App.IsConnecting) App.Client.InviteToGroup(address, user);
                        }

                        if (IsImageLoaded && !App.IsConnecting) UploadPhoto(path, address);
                    }
                    else throw new Exception();
                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(() => 
                    {
                        App.ConnectToServer();
                    });
                }
                finally
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        this.Close();
                    });
                }
            });

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
    }
}
