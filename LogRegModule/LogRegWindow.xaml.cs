using MessangerClient.Controls;
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
using System.Windows.Shapes;
using MessangerClient.ServiceReference;

namespace MessangerClient.LogRegModule
{
    /// <summary>
    /// Логика взаимодействия для LogRegWindow.xaml
    /// </summary>
    public partial class LogRegWindow : Window
    {

        User NewUser { get; set; }

        enum LogRegState
        {
            Login = 0,
            Reg1 = 1,
            Reg2 = 2
        }

        LogRegState state;

        LogRegState State { 
            get
            {
                return state;   
            }
            set
            {
                SetState(value);
            }
        }

        public LogRegWindow()
        {
            InitializeComponent();
            State = LogRegState.Login;
            NewUser = new User()
            {
                Username = String.Empty,
                Email = String.Empty,
                Password = String.Empty
            };
        }

        private void tbfirst_TextValueChanged(object sender, EventArgs e)
        {
            if (lbfirst.Visibility == Visibility.Visible) lbfirst.Visibility = Visibility.Hidden;
        }

        private void tbsecond_TextValueChanged(object sender, EventArgs e)
        {
            if (lbsecond.Visibility == Visibility.Visible) lbsecond.Visibility = Visibility.Hidden;
        }

        private void btnfirst_Click(object sender, RoutedEventArgs e)
        {
            switch (State)
            {
                case LogRegState.Login:
                    if (Validation())
                    {
                        string email = tbfirst.Text;
                        string password = tbsecond.Text;
                        Login(email,password);
                    }
                    break;

                case LogRegState.Reg1:
                    SetState(LogRegState.Login);
                    break;

                case LogRegState.Reg2:
                    SetState(LogRegState.Reg1);
                    break;
            }
        }

        private void btnsecond_Click(object sender, RoutedEventArgs e)
        {
            switch (State)
            {
                case LogRegState.Login:
                    SetState(LogRegState.Reg1);
                    break;

                case LogRegState.Reg1:
                    if (Validation())
                    {
                        NewUser.Username = tbfirst.Text;
                        NewUser.Email = tbsecond.Text;
                        SetState(LogRegState.Reg2);
                    }
                    break;

                case LogRegState.Reg2:
                    if (Validation()) 
                    {
                        NewUser.Password = tbfirst.Text;
                        NewUser.CreationDate = DateTime.Now;
                        Registrate();
                    }
                    break;
            }
        }


        private void SetLabelContent(Label l, string content)
        {
            l.Content = content;
            l.Visibility = Visibility.Visible;
        }


        private bool Validation()
        {
            bool IsAllGood = true;

            //if fields are empty

            if(tbfirst.Text == String.Empty)
            {
                SetLabelContent(lbfirst, "You need to fill this field");
                IsAllGood = false;
            }
            if(tbsecond.Text == String.Empty)
            {
                SetLabelContent(lbsecond, "You need to fill this field");
                IsAllGood = false;
            }

            if(IsAllGood)
            {
                switch (State)
                {
                    case LogRegState.Login:
                        if(!tbfirst.Text.Contains("@"))
                        {
                            SetLabelContent(lbfirst, "You need to enter your email");
                            IsAllGood = false;
                        }
                        if(tbsecond.Text.Length < 8)
                        {
                            SetLabelContent(lbsecond, "Password must have more than 7 symbols");
                            IsAllGood = false;
                        }
                        break;

                    case LogRegState.Reg1:
                        if(tbfirst.Text.Length < 8)
                        {
                            SetLabelContent(lbfirst, "Username must have more than 7 symbols");
                            IsAllGood = false;
                        }
                        if (!tbsecond.Text.Contains("@"))
                        {
                            SetLabelContent(lbsecond, "You need to enter your email");
                            IsAllGood = false;
                        }
                        break;

                    case LogRegState.Reg2:
                        if (tbfirst.Text.Length < 8)
                        {
                            SetLabelContent(lbfirst, "Password must have more than 7 symbols");
                            IsAllGood = false;
                        }
                        if(tbsecond.Text != tbfirst.Text)
                        {
                            SetLabelContent(lbsecond, "You need to confirm password");
                            IsAllGood = false;
                        }
                        break;
                }

            }
            

            return IsAllGood;
        }


        private void SetState(LogRegState state)
        {
            switch (state)
            {
                case LogRegState.Login:
                    tbfirst.PasswordMode = false;
                    tbsecond.PasswordMode = true;
                    tbfirst.DefaultText = "Email";
                    tbsecond.DefaultText = "Password";
                    tbfirst.Text = String.Empty;
                    tbsecond.Text = String.Empty;
                    lbheader.Content = "Login";
                    btnfirst.Content = "Sumbit";
                    btnsecond.Content = "Registrate";
                    lbfirst.Visibility = Visibility.Hidden;
                    lbsecond.Visibility = Visibility.Hidden;
                    break;

                case LogRegState.Reg1:
                    tbfirst.PasswordMode = false;
                    tbsecond.PasswordMode = false;
                    tbfirst.DefaultText = "Name";
                    tbsecond.DefaultText = "Email";
                    tbfirst.Text = NewUser.Username;
                    tbsecond.Text = NewUser.Email;
                    lbheader.Content = "Registration";
                    btnfirst.Content = "Back";
                    btnsecond.Content = "Next";
                    lbfirst.Visibility = Visibility.Hidden;
                    lbsecond.Visibility = Visibility.Hidden;
                    break;

                case LogRegState.Reg2:
                    tbfirst.PasswordMode = true;
                    tbsecond.PasswordMode = true;
                    tbfirst.DefaultText = "Password";
                    tbsecond.DefaultText = "Confirm password";
                    tbfirst.Text = NewUser.Password;
                    tbsecond.Text = String.Empty;
                    lbheader.Content = "Registration";
                    btnfirst.Content = "Back";
                    btnsecond.Content = "Finish";
                    lbfirst.Visibility = Visibility.Hidden;
                    lbsecond.Visibility = Visibility.Hidden;
                    break;
            }

            this.state = state;
        }


        private async void Login(string email, string password)
        {
            await Task.Run(() => {
                try
                {
                    User dbUser;
                    User1 user;
                    ResultCodes result = App.Client.Login(email, password, out user);
                    switch (result)
                    {
                        case ResultCodes.IncorrectEmail:
                            this.Dispatcher.Invoke(() => { SetLabelContent(lbfirst, "Incorrect email"); });
                            break;

                        case ResultCodes.IncorrectPassword:
                            this.Dispatcher.Invoke(() => { SetLabelContent(lbsecond, "Incorrect password"); });
                            break;

                        case ResultCodes.ServerError:
                            this.Dispatcher.Invoke(() =>
                            {
                                SetLabelContent(lbfirst, "Server error");
                                SetLabelContent(lbsecond, "Server error");
                            });
                            break;

                        case ResultCodes.Success:
                            this.Dispatcher.Invoke(() => {
                                dbUser = new User() {
                                    Username = user.Username,
                                    Email = email,
                                    Password = password,
                                    LastOnline = user.LastOnline,
                                    Id = user.id
                                };
                                MainWindow mw = new MainWindow(dbUser, true);
                                mw.Show();
                                this.Close();
                            });
                            break;
                    }
                }
                catch (Exception ex)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        SetLabelContent(lbfirst, "Server connection lost");
                        SetLabelContent(lbsecond, "Server connection lost");
                    });
                    App.ConnectToServer();
                }

            });
        }

        private async void Registrate()
        {
            await Task.Run(() => { 

                try
                {
                    ResultCodes result = App.Client.Registration(NewUser);
                    
                    switch(result)
                    {

                        case ResultCodes.IncorrectEmail:
                            this.Dispatcher.Invoke(() => {
                                SetState(LogRegState.Reg1);
                                SetLabelContent(lbsecond, "Incorrect email"); 
                            });
                            break;

                        case ResultCodes.IncorrectUserName:
                            this.Dispatcher.Invoke(() =>
                            {
                                SetState(LogRegState.Reg1);
                                SetLabelContent(lbfirst, "this name is already taken");
                            });
                            break;

                        case ResultCodes.ServerError:
                            this.Dispatcher.Invoke(() =>
                            {
                                SetLabelContent(lbfirst, "Server error");
                                SetLabelContent(lbsecond, "Server error");
                            });
                            break;

                        case ResultCodes.Success:
                            this.Dispatcher.Invoke(() => {
                                User current = new User() { Username = NewUser.Username, LastOnline = NewUser.LastOnline.GetValueOrDefault() };
                                MainWindow mw = new MainWindow(current,true);
                                mw.Show();
                                this.Close();
                            });
                            break;
                    }
                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        SetLabelContent(lbfirst, "Server connection lost");
                        SetLabelContent(lbsecond, "Server connection lost");
                    });
                    App.ConnectToServer();
                }

            });
        }

    }
}
