using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для MessageControl.xaml
    /// </summary>
    public partial class MessageControl : UserControl
    {


        public bool IsMine
        {
            get { return (bool)GetValue(IsMineProperty); }
            set { SetValue(IsMineProperty, value); }
        }



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
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

        public MessageControl()
        {
            InitializeComponent();
        }


        //----------------------------ISMINE PROPERTY----------------------------
        public static readonly DependencyProperty IsMineProperty =
            DependencyProperty.Register("IsMine", typeof(bool), typeof(MessageControl), new PropertyMetadata(false, new PropertyChangedCallback(IsMineChanged)));

        private static void IsMineChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MessageControl mc = d as MessageControl;
            mc.SetIsMine((bool)e.NewValue);
        }

        private void SetIsMine(bool value)
        {
            if (value)
            {
                body.Background = new SolidColorBrush(Color.FromRgb(74, 191, 182));
                tbtext.Foreground = new SolidColorBrush(Colors.White);
                lbdate.Foreground = new SolidColorBrush(Color.FromRgb(230, 230, 230));
                lbname.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                body.Background = new SolidColorBrush(Colors.White);
                tbtext.Foreground = new SolidColorBrush(Colors.Black);
                lbdate.Foreground = new SolidColorBrush(Color.FromRgb(179, 179, 179));
                lbname.Foreground = new SolidColorBrush(Color.FromRgb(179, 179, 179));
            }
        }


        //----------------------------TEXT PROPERTY----------------------------
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MessageControl), new PropertyMetadata(String.Empty, new PropertyChangedCallback(TextChanged)));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MessageControl mc = d as MessageControl;
            mc.SetText((string)e.NewValue);
        }

        private void SetText(string val)
        {
            tbtext.Text = val;
        }


        //----------------------------HEADER PROPERTY----------------------------
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(MessageControl), new PropertyMetadata("You", new PropertyChangedCallback(HeaderChanged)));

        private static void HeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MessageControl mc = d as MessageControl;
            mc.SetHeader((string)e.NewValue);
        }

        private void SetHeader(string val)
        {
            lbname.Content = val;
        }

        //----------------------------SENDTIME PROPERTY----------------------------
        public static readonly DependencyProperty SendTimeProperty =
            DependencyProperty.Register("SendTime", typeof(DateTime), typeof(MessageControl), new PropertyMetadata(DateTime.Now, new PropertyChangedCallback(SendTimeChanged)));

        private static void SendTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MessageControl mc = d as MessageControl;
            mc.SetSendTime((DateTime)e.NewValue);
        }

        private void SetSendTime(DateTime date)
        {
            string time = date.ToString("dd MMM H:mm", CultureInfo.CreateSpecificCulture("en"));
            lbdate.Content = time;
        }
    }
}
