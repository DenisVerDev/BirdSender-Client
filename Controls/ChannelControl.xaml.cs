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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MessangerClient.Controls
{
    /// <summary>
    /// Логика взаимодействия для ChannelControl.xaml
    /// </summary>
    public partial class ChannelControl : UserControl
    {

        public delegate void ChannelDelegate(int id);
        public event ChannelDelegate ChannelSelected;   

        public string ChannelName
        {
            get { return (string)GetValue(ChannelNameProperty); }
            set { SetValue(ChannelNameProperty, value); }
        }



        public string LastMsg
        {
            get { return (string)GetValue(LastMsgProperty); }
            set { SetValue(LastMsgProperty, value); }
        }



        public int NewMessagesCount
        {
            get { return (int)GetValue(NewMessagesCountProperty); }
            set { SetValue(NewMessagesCountProperty, value); }
        }

        public int Id { get; set; }

        public bool IsGroup { get; set; }

        public string Address { get; set; }

        private bool isselected;
        public bool IsSelected 
        { 
            get
            {
                return isselected;
            }
            set
            {
                isselected = value;
                if (value == true)
                {
                    body.Background = new SolidColorBrush(Color.FromRgb(108, 196, 199));
                    lbname.Foreground = new SolidColorBrush(Color.FromRgb(13, 150, 140));
                    lblast.Foreground = new SolidColorBrush(Colors.White);
                }
                else
                {
                    body.Background = new SolidColorBrush(Colors.White);
                    lbname.Foreground = new SolidColorBrush(Color.FromRgb(43, 166, 157));
                    lblast.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
        }

        public ChannelControl()
        {
            InitializeComponent();
            IsSelected = false;
        }

        private void body_MouseEnter(object sender, MouseEventArgs e)
        {
            if(!IsSelected) body.Background = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        }

        private void body_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsSelected) body.Background = new SolidColorBrush(Colors.White);
        }

        private void body_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (ChannelSelected != null)
                {
                    ChannelSelected(this.Id);
                }
            }

        }

        //-------------------------CHANNELNAME PROPERTY-----------------------------------------
        public static readonly DependencyProperty ChannelNameProperty =
            DependencyProperty.Register("ChannelName", typeof(string), typeof(ChannelControl), new PropertyMetadata("Test", new PropertyChangedCallback(ChannelNameChanged)));

        private static void ChannelNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChannelControl c = d as ChannelControl;
            c.SetChannelName((string)e.NewValue);
        }

        private void SetChannelName(string value)
        {
            lbname.Content = value;
        }

        //-------------------------LASTMSG PROPERTY-----------------------------------------
        public static readonly DependencyProperty LastMsgProperty =
            DependencyProperty.Register("LastMsg", typeof(string), typeof(ChannelControl), new PropertyMetadata(String.Empty, new PropertyChangedCallback(LastMsgChanged)));

        private static void LastMsgChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChannelControl c = d as ChannelControl;
            c.SetLastMsg((string)e.NewValue);
        }

        private void SetLastMsg(string value)
        {
            lblast.Content = value;
        }

        //-------------------------NEWMESSAGESCOUNT PROPERTY-----------------------------------------
        public static readonly DependencyProperty NewMessagesCountProperty =
            DependencyProperty.Register("NewMessagesCount", typeof(int), typeof(ChannelControl), new PropertyMetadata(0, new PropertyChangedCallback(NewMessagesCountChanged)));

        private static void NewMessagesCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChannelControl cc = d as ChannelControl;
            cc.SetNewMsgCount((int)e.NewValue);
        }

        private void SetNewMsgCount(int value)
        {
            if (value > 0)
            {
                val.Visibility = Visibility.Visible;
                lbval.Content = value.ToString();
            }
            else val.Visibility = Visibility.Hidden;
        }
    }
}
