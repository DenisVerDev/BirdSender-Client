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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MessangerClient.Controls
{
    /// <summary>
    /// Логика взаимодействия для TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl
    {


        public string TopBarHeader
        {
            get { return (string)GetValue(TopBarHeaderProperty); }
            set { SetValue(TopBarHeaderProperty, value); }
        }



        public bool ResizeMode
        {
            get { return (bool)GetValue(ResizeModeProperty); }
            set { SetValue(ResizeModeProperty, value); }
        }



        public bool HideMode
        {
            get { return (bool)GetValue(HideModeProperty); }
            set { SetValue(HideModeProperty, value); }
        }



        public bool CanMove
        {
            get { return (bool)GetValue(CanMoveProperty); }
            set { SetValue(CanMoveProperty, value); }
        }

        public double PrevW { get; set; }
        public double PrevH { get; set; }

        public double PrevTop { get; set; }
        public double PrevLeft { get; set; }

        public TopBar()
        {
            InitializeComponent();
        }

        private void topbar_MouseDown(object sender, MouseButtonEventArgs e) //drag window
        {
            if (e.ChangedButton == MouseButton.Left && CanMove)
            {
                Window w = Window.GetWindow(this);
                if (w != null)
                {
                    w.DragMove();
                }
            }
        }

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            Window w = Window.GetWindow(this);
            if(w != null) w.Close();
        }

        private void btnsizemode_Click(object sender, RoutedEventArgs e)
        {
            Window w = Window.GetWindow(this);
            if (w != null)
            {
                if (w.Width != w.MaxWidth)
                {
                    PrevTop = w.Top;
                    PrevLeft = w.Left;
                    PrevH = w.Height;
                    PrevW = w.Width;
                    w.Height = w.MaxHeight;
                    w.Width = w.MaxWidth;
                    w.Top = 0;
                    w.Left = 0;
                }
                else
                {
                    w.Height = PrevH;
                    w.Width = PrevW;
                    w.Top = PrevTop;
                    w.Left = PrevLeft;
                }
            }
        }

        private void btnhide_Click(object sender, RoutedEventArgs e)
        {
            Window w = Window.GetWindow(this);
            if(w != null) w.WindowState = WindowState.Minimized;
        }



        //-------------------------RESIZEMODE PROPERTY------------------------------
        public static readonly DependencyProperty ResizeModeProperty =
            DependencyProperty.Register("ResizeMode", typeof(bool), typeof(TopBar), new PropertyMetadata(true, new PropertyChangedCallback(ResizeModeChanged)));

        private static void ResizeModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TopBar topBar = d as TopBar;
            topBar.SetResizeMode((bool)e.NewValue);
        }

        private void SetResizeMode(bool value)
        {
            if (value) btnsizemode.Visibility = Visibility.Visible;
            else btnsizemode.Visibility = Visibility.Collapsed;
        }

        //-------------------------HIDEMODE PROPERTY------------------------------
        public static readonly DependencyProperty HideModeProperty =
            DependencyProperty.Register("HideMode", typeof(bool), typeof(TopBar), new PropertyMetadata(true, new PropertyChangedCallback(HideModeChanged)));

        private static void HideModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TopBar topBar = d as TopBar;
            topBar.SetHideMode((bool)e.NewValue);
        }

        private void SetHideMode(bool val)
        {
            if (val) btnhide.Visibility = Visibility.Visible;
            else btnhide.Visibility = Visibility.Collapsed;
        }

        //-------------------------HEADER PROPERTY------------------------------
        public static readonly DependencyProperty TopBarHeaderProperty =
            DependencyProperty.Register("TopBarHeader", typeof(string), typeof(TopBar), new PropertyMetadata("Header", new PropertyChangedCallback(HeaderChanged)));

        private static void HeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TopBar topBar = d as TopBar;
            topBar.SetHedear(e.NewValue.ToString());
        }

        private void SetHedear(string header)
        {
            this.header.Content = header;
        }

        //-------------------------CANMOVE PROPERTY------------------------------
        public static readonly DependencyProperty CanMoveProperty =
            DependencyProperty.Register("CanMove", typeof(bool), typeof(TopBar), new PropertyMetadata(true));
    }
}
