using MessangerClient.ServiceReference;
using NAudio.Wave;
using SharpVectors.Converters;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MessangerClient.Controls
{
    /// <summary>
    /// Логика взаимодействия для UserEnterMsg.xaml
    /// </summary>
    public partial class UserEnterMsg : System.Windows.Controls.UserControl
    {
        public bool IsRecordingSound { get; set; }

        private bool IsSending { get; set; }

        public delegate void MessageInput(string data, DateTime sendTime);
        public event MessageInput NewTextMessage;
        public event MessageInput NewFileMessage;

        public string Address { get; set; }
        public bool IsGroup { get; set; }

        public WaveFileWriter waveFile = null;

        public string FileName { get; set; }

        public UserEnterMsg()
        {
            InitializeComponent();
            IsRecordingSound = false;
            IsSending = false;
            App.Device.DataAvailable += Device_DataAvailable;
            App.Device.RecordingStopped += Device_RecordingStopped;
        }

        private void Device_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if(waveFile != null)
            {
                waveFile.Close();
                waveFile.Dispose();
                waveFile = null;
                if (IsSending)
                {
                    IsSending = false;
                    if (NewFileMessage != null) NewFileMessage(System.IO.Path.GetFullPath(FileName), DateTime.Now);
                }
                else File.Delete(FileName);
                
            }
        }

        private void Device_DataAvailable(object sender, WaveInEventArgs e)
        {

            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        private void tbmsg_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Enter && tbmsg.Text != String.Empty)
            {
                if(NewTextMessage != null) NewTextMessage(tbmsg.Text, DateTime.Now);
            }
        }

        private void btnfiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if(fd.ShowDialog() == DialogResult.OK)
            {
                if(NewFileMessage != null) NewFileMessage(fd.FileName, DateTime.Now);
            }
        }

        private void btnmic_Click(object sender, RoutedEventArgs e)
        {
            Storyboard s = Resources["smic"] as Storyboard;
            if (!IsRecordingSound)
            {
                s.Begin(btnmic.Template.FindName("body", btnmic) as Border, true);
                (btnmic.Content as SvgViewbox).Width = 16;
                (btnmic.Content as SvgViewbox).Height = 16;
                (btnmic.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnmicpause.svg");
                IsRecordingSound = true;
                DateTime time = DateTime.Now;
                string filename = String.Format("{0}_{1}_{2}", time.ToShortDateString(), time.ToLongTimeString(), time.Millisecond);
                filename = filename.Replace(":", "_");
                filename = filename.Replace(".", "_");
                filename += ".wav";
                string mode = "Chats";
                if (IsGroup) mode = "Groups";
                FileName = String.Format("ClientData/{0}/{1}/files/{2}", mode, Address, filename);
                if(!Directory.Exists(FileName)) FileName = String.Format("ClientData/temporary files/{0}", filename);
                waveFile = new WaveFileWriter(FileName, App.Device.WaveFormat);
                App.Device.StartRecording();
            }
            else
            {
                StopAnim(s);
                IsSending = true;
                App.Device.StopRecording();
                IsRecordingSound = false;
            }
        }

        public void StopRecording()
        {
            if (waveFile != null)
            {
                Storyboard s = Resources["smic"] as Storyboard;
                StopAnim(s);
                App.Device.StopRecording();
                IsRecordingSound = false;
            }
        }

        private void StopAnim(Storyboard s)
        {
            s.Stop(btnmic.Template.FindName("body", btnmic) as Border);
            (btnmic.Content as SvgViewbox).Width = 28;
            (btnmic.Content as SvgViewbox).Height = 28;
            (btnmic.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnmic.svg");
        }

        private void btnmic_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(!IsRecordingSound) (btnmic.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnmic2.svg");
        }

        private void btnmic_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsRecordingSound) (btnmic.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnmic.svg");
        }

        public void ClearText()
        {
            tbmsg.Text = String.Empty;
        }
    }
}
