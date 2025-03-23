using MessangerClient.ServiceReference;
using NAudio.Wave;
using SharpVectors.Converters;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для FileControl.xaml
    /// </summary>
    public partial class FileControl : UserControl
    {
        public bool IsMine
        {
            get { return (bool)GetValue(IsMineProperty); }
            set { SetValue(IsMineProperty, value); }
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public DateTime SendTime
        {
            get { return (DateTime)GetValue(SendTimeProperty); }
            set { SetValue(SendTimeProperty, value); }
        }

        public enum FileType
        {
            File = 0,
            Audio = 1,
            Upload=2,
            Load=3,
            Reload=4
        }

        public FileType ControlType
        {
            get { return (FileType)GetValue(ControlTypeProperty); }
            set { SetValue(ControlTypeProperty, value); }
        }



        public string FileInfo
        {
            get { return (string)GetValue(FileInfoProperty); }
            set { SetValue(FileInfoProperty, value); }
        }

        public string FileLink { get; set; }

        public Channel Channel { get; set; }

        public bool IsWorking { get; set; }

        public int Id { get; set; }

        public long FileLength { get; set; }

        public delegate void FileDel(int id);
        public event FileDel SendCancel;

        public long LastPositon { get; set; }

        public static WaveOut output = null;
        public static AudioFileReader a = null;
        public static string CurrentFile { get;  private set; }

        public static event Action ValueClear;

        public enum State
        {
            Load = 0,
            Upload = 1
        }

        State StateType;

        public FileControl()
        {
            InitializeComponent();
            LastPositon = 0;
            pbinfo.Value = 0;
            ValueClear += FileControl_ValueChanged;
        }

        private void FileControl_ValueChanged()
        {
            if (ControlType == FileType.Audio)
            {
                pbinfo.Value = 0;
                (btnact.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnplay.svg");
            }
        }

        private void btnact_Click(object sender, RoutedEventArgs e)
        {
            switch (ControlType)
            {
                case FileType.Audio:
                    if (File.Exists(FileInfo))
                    {
                        try
                        {
                            if (output == null) output = new WaveOut();

                            if(CurrentFile != FileInfo) //if user start playing other sound, we stop previous
                            {
                                CurrentFile = FileInfo;
                                if (output.PlaybackState != PlaybackState.Stopped)
                                {
                                    output.Stop();
                                    if (ValueClear != null) ValueClear();
                                }
                                if (a != null) a.Close();
                            }

                            if (output.PlaybackState == PlaybackState.Playing)
                            {
                                (btnact.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnplay.svg");
                                output.Pause();
                            }
                            else if (output.PlaybackState == PlaybackState.Paused)
                            {
                                (btnact.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnpause.svg");
                                output.Resume();
                                AudioPb();
                            }
                            else
                            {
                                a = new AudioFileReader(FileInfo);
                                output.Init(a);
                                a.Seek(0, SeekOrigin.Begin);
                                (btnact.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnpause.svg");
                                output.Play();
                                AudioPb();
                            }
                        }
                        catch(Exception ex)
                        {

                        }

                    }
                    break;

                case FileType.File:
                    if(File.Exists(FileInfo)) System.Diagnostics.Process.Start(System.IO.Path.GetFullPath(FileInfo));
                    break;

                case FileType.Upload:
                    if (IsWorking)
                    {
                        IsWorking = false;
                        if (SendCancel != null) SendCancel(Id);
                    }
                    break;

                case FileType.Load:
                    if (!App.IsConnecting && IsWorking == false)
                    {
                        (btnact.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btncancel.svg");
                        DownloadFile();
                    }
                    else
                    {
                        IsWorking = false;
                        (btnact.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnload.svg");
                    }
                    break;

                case FileType.Reload:
                    if (!App.IsConnecting && StateType == State.Upload) UploadFile();
                    else if (!App.IsConnecting && StateType == State.Load) DownloadFile();
                    break;
            }

        }

        public async void AudioPb()
        {
            await Task.Run(() => { 
                try
                {
                    bool isallgood = true;
                    while (a.CurrentTime < a.TotalTime && output.PlaybackState == PlaybackState.Playing && isallgood)
                    {
                        this.Dispatcher.Invoke(() => 
                        {
                            if (CurrentFile != FileInfo) isallgood = false;
                            else PbCalculation(a.TotalTime, a.CurrentTime);
                        });
                    }
                    if (output.PlaybackState != PlaybackState.Paused && isallgood)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            pbinfo.Value = 0;
                            (btnact.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnplay.svg");
                            a.Close();
                            a.Dispose();
                        });
                    }
                }
                catch(Exception ex)
                {

                }
            });
        }

        public static void StopPlaying()
        {
            if (output != null)
            {
                output.Stop();
                a.Close();
                a.Dispose();
            }
        }

        private void PbCalculation(long total, long pos)
        {
            double percent = (double)(pos * 100 / total);
            pbinfo.Value = percent;
        }

        private void PbCalculation(TimeSpan total, TimeSpan pos)
        {
            double percent = (double)(pos.TotalSeconds * 100 / total.TotalSeconds);
            pbinfo.Value = percent;
        }

        public async void UploadFile()
        {
            StateType = State.Upload;
            ControlType = FileType.Upload;
            IsWorking = true;
            string FilePath = FileInfo;
            DateTime time = SendTime;
            await Task.Run(() => {
                try
                {
                    string filename = String.Format("{0}_{1}_{2}", time.ToShortDateString(), time.ToLongTimeString(), time.Millisecond);
                    filename = filename.Replace(":", "_");
                    filename = filename.Replace(".", "_");
                    filename += System.IO.Path.GetExtension(FilePath);
                    using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                    {
                        FileLength = fs.Length;
                        fs.Position = LastPositon;
                        while (LastPositon < fs.Length)
                        {
                            if (IsWorking)
                            {
                                byte[] data = new byte[65000];
                                fs.Read(data, 0, data.Length);
                                if (!App.IsConnecting)
                                {
                                    App.Client.SendFile(filename, Channel.Address, LastPositon, data);
                                    LastPositon = fs.Position;
                                    this.Dispatcher.Invoke(() => { PbCalculation(fs.Length, fs.Position); });
                                }
                                else
                                {
                                    this.Dispatcher.Invoke(() => { ControlType = FileType.Reload; });
                                    break;
                                }
                            }
                            else break;
                            
                        }
                        this.Dispatcher.Invoke(() => {
                            if (LastPositon >= FileLength)
                            {
                                InitializeType();

                                FileLink = filename;

                                Message msg = new Message();
                                msg.Address = Channel.Address;
                                msg.SendTime = SendTime.ToString();
                                msg.Username = App.CurrentUser.Username;
                                msg.Text = String.Format("[file link={0}, length={1}]{2}", FileLink, FileLength.ToString(), FileInfo);

                                Channel.SendMessage(msg);
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    this.Dispatcher.Invoke(() => 
                    {
                        ControlType = FileType.Reload;
                        App.ConnectToServer(); 
                    });
                }
            });
            IsWorking = false;
        }

        public async void DownloadFile()
        {
            StateType = State.Load;
            string FilePath = FileInfo;
            IsWorking = true;
            string address = Channel.Address;
            await Task.Run(() => {
                try
                {
                    using (FileStream fs = new FileStream(FilePath, FileMode.Append, FileAccess.Write))
                    {
                        while (LastPositon < FileLength && IsWorking)
                        {
                            byte[] data = null;
                            if (!App.IsConnecting)
                            {
                                ResultCodes result = App.Client.DownloadFile(FileLink, address, LastPositon, out data);
                                if ( result == ResultCodes.Success)
                                {
                                    fs.Write(data, 0, data.Length);
                                    LastPositon += data.Length;
                                    this.Dispatcher.Invoke(() => { PbCalculation(FileLength, LastPositon); });
                                }
                                else break;
                            }
                            else break;
                        }
                        fs.Close();
                    }
                    this.Dispatcher.Invoke(() => { InitializeType(); });
                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(() => { App.ConnectToServer(); });
                }
            });
        }

        public void InitializeType()
        {
            if (File.Exists(FileInfo))
            {
                FileInfo fi = new FileInfo(FileInfo);
                if (fi.Length >= FileLength)
                {
                    string extension = System.IO.Path.GetExtension(FileInfo);
                    if (extension == ".mp3" || extension == ".wav" || extension == ".ogg")
                    {
                        ControlType = FileType.Audio;
                    }
                    else ControlType = FileType.File;
                }
                else
                {
                    LastPositon = fi.Length;
                    PbCalculation(FileLength, LastPositon);
                    ControlType = FileType.Load;
                }
            }
            else ControlType = FileType.Load;
        }

        //----------------------------ISMINE PROPERTY----------------------------
        public static readonly DependencyProperty IsMineProperty =
            DependencyProperty.Register("IsMine", typeof(bool), typeof(FileControl), new PropertyMetadata(false, new PropertyChangedCallback(IsMineChanged)));

        private static void IsMineChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FileControl mc = d as FileControl;
            mc.SetIsMine((bool)e.NewValue);
        }

        private void SetIsMine(bool value)
        {
            if (value)
            {
                body.Background = new SolidColorBrush(Color.FromRgb(74, 191, 182));
                btnact.Background = new SolidColorBrush(Colors.White);
                lbdate.Foreground = new SolidColorBrush(Color.FromRgb(230, 230, 230));
                lbname.Foreground = new SolidColorBrush(Colors.White);
                tbfileinfo.Foreground = new SolidColorBrush(Colors.White);
                pbinfo.BorderBrush = new SolidColorBrush(Colors.White);
                pbinfo.Background = new SolidColorBrush(Colors.LightGray);
            }
            else
            {
                body.Background = new SolidColorBrush(Colors.White);
                btnact.Background = new SolidColorBrush(Color.FromRgb(179,179,179));
                lbdate.Foreground = new SolidColorBrush(Color.FromRgb(179, 179, 179));
                lbname.Foreground = new SolidColorBrush(Color.FromRgb(179, 179, 179));
                tbfileinfo.Foreground = new SolidColorBrush(Color.FromRgb(179, 179, 179));
                pbinfo.BorderBrush = new SolidColorBrush(Color.FromRgb(230, 230, 230));
            }
        }

        //----------------------------HEADER PROPERTY----------------------------
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(FileControl), new PropertyMetadata("You", new PropertyChangedCallback(HeaderChanged)));

        private static void HeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FileControl mc = d as FileControl;
            mc.SetHeader((string)e.NewValue);
        }

        private void SetHeader(string val)
        {
            lbname.Content = val;
        }

        //----------------------------SENDTIME PROPERTY----------------------------
        public static readonly DependencyProperty SendTimeProperty =
            DependencyProperty.Register("SendTime", typeof(DateTime), typeof(FileControl), new PropertyMetadata(DateTime.Now, new PropertyChangedCallback(SendTimeChanged)));

        private static void SendTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FileControl mc = d as FileControl;
            mc.SetSendTime((DateTime)e.NewValue);
        }

        private void SetSendTime(DateTime date)
        {
            string time = date.ToString("dd MMM H:mm", CultureInfo.CreateSpecificCulture("en"));
            lbdate.Content = time;
        }

        //----------------------------CONTROLTYPE PROPERTY----------------------------
        public static readonly DependencyProperty ControlTypeProperty =
            DependencyProperty.Register("ControlType", typeof(FileType), typeof(FileControl), new PropertyMetadata(FileType.Upload, new PropertyChangedCallback(ControlTypeChanged)));

        private static void ControlTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FileControl fc = d as FileControl;
            fc.SetControlType((FileType)e.NewValue);
        }

        private void SetControlType(FileType val)
        {
            switch (val)
            {
                case FileType.File:
                    tbfileinfo.Visibility = Visibility.Visible;
                    pbinfo.Visibility = Visibility.Collapsed;
                    pbinfo.Value = 0;
                    (btnact.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnfilemsg.svg");
                    break;

                case FileType.Audio:
                    tbfileinfo.Visibility = Visibility.Collapsed;
                    pbinfo.Visibility = Visibility.Visible;
                    pbinfo.Value = 0;
                    (btnact.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnplay.svg");
                    break;

                case FileType.Upload:
                    tbfileinfo.Visibility = Visibility.Collapsed;
                    pbinfo.Visibility = Visibility.Visible;
                    (btnact.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btncancel.svg");
                    StateType = State.Upload;
                    break;

                case FileType.Load:
                    tbfileinfo.Visibility = Visibility.Collapsed;
                    pbinfo.Visibility = Visibility.Visible;
                    pbinfo.Value = 0;
                    (btnact.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnload.svg");
                    StateType = State.Load;
                    break;

                case FileType.Reload:
                    tbfileinfo.Visibility = Visibility.Collapsed;
                    pbinfo.Visibility = Visibility.Visible;
                    (btnact.Content as SvgViewbox).Source = new Uri("pack://application:,,,/Resources/btnreload.svg");
                    break;
            }

        }

        //----------------------------FILEINFO PROPERTY----------------------------
        public static readonly DependencyProperty FileInfoProperty =
            DependencyProperty.Register("FileInfo", typeof(string), typeof(FileControl), new PropertyMetadata(String.Empty, new PropertyChangedCallback(FileInfoChanged)));

        private static void FileInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FileControl fc = d as FileControl;
            fc.SetFileInfo((string)e.NewValue);
        }

        private void SetFileInfo(string val)
        {
            tbfileinfo.Text = System.IO.Path.GetFileName(val);
        }
    }
}
