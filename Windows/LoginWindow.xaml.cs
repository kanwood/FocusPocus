using FocusPocus.DataBase;
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

namespace FocusPocus.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private bool _isUnsuccessfully = false;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            string loginValue = LoginBox.Text;
            string passValue = PassBox.Password.ToString();

            if (loginValue == string.Empty || passValue == string.Empty)
            {
                MessageBox.Show("не все поля заполнены");
            }

            else
            {
                if (_isUnsuccessfully)
                {
                    Captcha captcha = new Captcha();
                    captcha.ShowDialog();

                    if (!captcha.isCompleted)
                    {
                        MessageBox.Show("Для продолжения авторизации необходимо пройти капчу");
                        return;
                    }

                }

                List<User> foundUser = TradeEntities.GetContext().User.Where(u => u.UserLogin == loginValue && u.UserPassword == passValue).ToList();

                if (foundUser.Count <= 0)
                {
                    MessageBox.Show("Пользователь не найден");
                    _isUnsuccessfully = true;
                }

                else
                {

                    if (foundUser.Count > 0)
                    {
                        TradeEntities.GetContext().Username = foundUser[0].UserLogin;
                        if (foundUser[0].UserID == 1)
                        {
                            MenuAdmin menuAdmin = new MenuAdmin(foundUser[0]); // Передаем пользователя
                            menuAdmin.Show();
                            this.Close();
                        }
                        else
                        {
                            MenuClient menuClient = new MenuClient(foundUser[0]);
                            menuClient.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                    }
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}

