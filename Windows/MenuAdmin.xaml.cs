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
    /// Логика взаимодействия для MenuAdmin.xaml
    /// </summary>
    /// 
    public class CartItem
{
    public Product Product { get; set; } // Товар
    public int Quantity { get; set; }    // Количество


    }
    public partial class MenuAdmin : Window
    {
        private User _loggedUser; // Поле для хранения данных пользователя
        private List<Product> products; // Список всех товаров
        private List<CartItem> cartItems = new List<CartItem>(); // Корзина
        List<Product> filterproduct = new List<Product>(); // Список для фильтрации

        public MenuAdmin(User loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser; // Сохраняем переданного пользователя
            DataLoad();
        }

        private void ViewCart_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new CartWindow(cartItems);
            cartWindow.ShowDialog();
        }
        private void DataLoad()
        {
            products = TradeEntities.GetContext().Product.ToList(); // Инициализируем список всех товаров
            DGAdmin.ItemsSource = products; // Отображаем все товары в DataGrid
        }

        // Обработчик кнопки "Добавить в корзину"
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var selectedProduct = button.Tag as Product; // Получаем товар из Tag кнопки
            if (selectedProduct == null) return;

            // Проверяем, есть ли товар уже в корзине
            var existingCartItem = cartItems.FirstOrDefault(item => item.Product.ProductID == selectedProduct.ProductID);
            if (existingCartItem != null)
            {
                // Если товар уже в корзине, увеличиваем количество
                existingCartItem.Quantity++;
            }
            else
            {
                // Если товара нет в корзине, добавляем его
                cartItems.Add(new CartItem { Product = selectedProduct, Quantity = 1 });
            }

            MessageBox.Show("Товар добавлен в корзину!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }



        private void search_Click(object sender, RoutedEventArgs e)
        {
            filterproduct.Clear();

            string productSearch = searchTB.Text.ToLower(); // Приводим поисковый запрос к нижнему регистру

            if (string.IsNullOrEmpty(productSearch))
            {
                // Если строка поиска пустая, отображаем все товары
                DGAdmin.ItemsSource = products;
            }
            else
            {
                // Ищем по артикулу, названию и описанию
                filterproduct = products
                    .Where(product =>
                        product.ProductArticleNumber.ToLower().Contains(productSearch) || // Поиск по артикулу
                        product.ProductName.ToLower().Contains(productSearch) ||          // Поиск по названию
                        product.ProductDescription.ToLower().Contains(productSearch)      // Поиск по описанию
                    ) // Закрываем скобку для метода Where
                    .ToList(); // Преобразуем результат в список

                DGAdmin.ItemsSource = filterproduct;
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            searchTB.Text = string.Empty; // Очищаем текстовое поле поиска
            DGAdmin.ItemsSource = products; // Отображаем все товары
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct(null);
            addProduct.Show();
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = DGAdmin.SelectedItem as Product;

            if (selectedUser == null)
            {
                MessageBox.Show("Выберите товар для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TradeEntities.GetContext().Product.Remove(selectedUser);
            TradeEntities.GetContext().SaveChanges();

            DataLoad();

            MessageBox.Show("Товар успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный товар
            var selectedProduct = DGAdmin.SelectedItem as Product;

            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите товар для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Открываем окно редактирования
            AddProduct addProduct = new AddProduct(selectedProduct);
            addProduct.Show();
            this.Close();
        }
    }
}