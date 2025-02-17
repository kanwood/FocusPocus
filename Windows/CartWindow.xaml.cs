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
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow(List<CartItem> cartItems)
        {
            InitializeComponent();
            CartListBox.ItemsSource = cartItems; // Привязываем список товаров в корзине
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заказ оформлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}