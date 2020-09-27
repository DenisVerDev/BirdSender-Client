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
    /// Логика взаимодействия для ServiceControl.xaml
    /// </summary>
    public partial class ServiceControl : UserControl
    {


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public DateTime SendTime { get; set; }

        public ServiceControl()
        {
            InitializeComponent();
        }


        //-----------------------TEXT PROPERTY-----------------------------------------
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ServiceControl), new PropertyMetadata(String.Empty, new PropertyChangedCallback(TextChanged)));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ServiceControl sc = d as ServiceControl;
            sc.SetText((string)e.NewValue);
        }

        private void SetText(string val)
        {
            lbcontent.Content = val;
        }
    }
}
