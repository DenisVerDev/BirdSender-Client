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
    /// Логика взаимодействия для BirdTextBox.xaml
    /// </summary>
    public partial class BirdTextBox : UserControl
    {


        public string Text
        {
            get
            {
                if (!PasswordMode) return tb.Text;
                else return pb.Password;
            }
            set
            {
                tb.Text = value;
                pb.Password = value;
            }
        }



        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }

        public bool PasswordMode
        {
            get { return (bool)GetValue(PasswordModeProperty); }
            set { SetValue(PasswordModeProperty, value); }
        }



        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public delegate void TextEvents(object sender, EventArgs e);
        public event TextEvents TextValueChanged;

        public delegate void ActionStarted(string text);
        public event ActionStarted EnterPressed;
        public BirdTextBox()
        {
            InitializeComponent();
            Text = String.Empty;
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
                if (Text == String.Empty) placeholder.Visibility = Visibility.Visible;
                else placeholder.Visibility = Visibility.Hidden;

                if (TextValueChanged != null)
                {
                    TextValueChanged(sender, e);
                }
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(EnterPressed != null)
                {
                    EnterPressed(Text);
                }
            }
        }

        //-------------------------------------DEFAULT TEXT PROPERTY----------------------------
        public static readonly DependencyProperty DefaultTextProperty =
            DependencyProperty.Register("DefaultText", typeof(string), typeof(BirdTextBox), new PropertyMetadata(String.Empty, new PropertyChangedCallback(DefaultTextChanged)));

        private static void DefaultTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BirdTextBox b = d as BirdTextBox;
            b.SetDefaultText((string)e.NewValue);
        }

        private void SetDefaultText(string value)
        {
            placeholder.Content = value;
        }


        //-------------------------------------PASSWORD MODE PROPERTY----------------------------
        public static readonly DependencyProperty PasswordModeProperty =
            DependencyProperty.Register("PasswordMode", typeof(bool), typeof(BirdTextBox), new PropertyMetadata(false, new PropertyChangedCallback(PasswordModeChanged)));

        private static void PasswordModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BirdTextBox b = d as BirdTextBox;
            b.SetPasswordMode((bool)e.NewValue);
        }

        private void SetPasswordMode(bool value)
        {
            if (value)
            {
                tb.Visibility = Visibility.Hidden;
                tb.IsEnabled = false;
                pb.Visibility = Visibility.Visible;
                pb.IsEnabled = true;
                pb.Password = Text;
            }
            else
            {
                tb.Visibility = Visibility.Visible;
                tb.IsEnabled = true;
                pb.Visibility = Visibility.Hidden;
                pb.IsEnabled = false;
                tb.Text = Text;
            }
        }

        //-------------------------------------MAXLENGTH PROPERTY----------------------------
        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(BirdTextBox), new PropertyMetadata(20, new PropertyChangedCallback(MaxLengthChanged)));

        private static void MaxLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BirdTextBox b = d as BirdTextBox;
            b.SetMaxLength((int)e.NewValue);
        }

        private void SetMaxLength(int value)
        {
            tb.MaxLength = value;
            pb.MaxLength = value;
        }

        private bool IsOnlyLatin(char ch)
        {
                if ((int)ch <= 97 || (int)ch >= 122)
                {
                    return false;
                }
                return true;
        }
    }
}
