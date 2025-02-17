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
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        private Product _currentProduct = new Product();
        public AddProduct(Product selectedProduct)
        {
            InitializeComponent();
            LoadData();

            if (selectedProduct != null)
                _currentProduct = selectedProduct;
            else
                _currentProduct.ProductPhoto = @"..\Images\picture.png"; // Устанавливаем путь по умолчанию

            DataContext = _currentProduct;
        }

        private void LoadData()
        {
            Manufacturer.ItemsSource = TradeEntities.GetContext().Manufacturer.ToList();
            Provider.ItemsSource = TradeEntities.GetContext().Provider.ToList();
            Category.ItemsSource = TradeEntities.GetContext().Category.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (_currentProduct.ProductID == 0)
            {
                // Устанавливаем путь к изображению по умолчанию, если он не задан
                if (string.IsNullOrEmpty(_currentProduct.ProductPhoto))
                    _currentProduct.ProductPhoto = @"..\Images\picture.png";

                TradeEntities.GetContext().Product.Add(_currentProduct);
            }

            try
            {
                TradeEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно обновлены!");
                MenuAdmin menuAdmin = new MenuAdmin(null);
                menuAdmin.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MenuAdmin menuAdmin = new MenuAdmin(null);
            menuAdmin.Show();
            this.Close();
        }
    }
}
