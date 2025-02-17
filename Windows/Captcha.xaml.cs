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
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
 
        private string _numbers = "0123456789";
        private string _letters = "abcdefghiuy";
        private string _randomCaptcha = "";
        public bool isCompleted;

        public Captcha()
        {
            InitializeComponent();


            Random random = new Random();

            for (int x = 0; x < 5; x++)
            {
                if (random.Next(0, 2) == 0) _randomCaptcha += _numbers[random.Next(0, _numbers.Length - 1)];
                else _randomCaptcha += _letters[random.Next(0, _letters.Length - 1)];
            }

            CapthaTB.Text = _randomCaptcha;
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserTB.Text;

            if (_randomCaptcha != userInput)
            {
                MessageBox.Show("Капча не пройдена!");
                isCompleted = false;

                this.Close();
            }
            else
            {
                MessageBox.Show("Капча пройдена!");
                isCompleted = true;

                this.Close();
            }

        }

    }
}
