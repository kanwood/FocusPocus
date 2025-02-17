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
    /// Логика взаимодействия для GuestWindow.xaml
    /// </summary>
    public partial class GuestWindow : Window
    {

        List<Product> products; // Список всех товаров
        List<Product> filterproduct = new List<Product>(); // Список для фильтрации

        public GuestWindow()
        {
            InitializeComponent();

            DataLoad();
        }

        private void DataLoad()
        {
            products = TradeEntities.GetContext().Product.ToList(); // Инициализируем список всех товаров
            DGGuest.ItemsSource = products; // Отображаем все товары в DataGrid
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            filterproduct.Clear();

            string productSearch = searchTB.Text.ToLower(); // Приводим поисковый запрос к нижнему регистру

            if (string.IsNullOrEmpty(productSearch))
            {
                // Если строка поиска пустая, отображаем все товары
                DGGuest.ItemsSource = products;
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

                DGGuest.ItemsSource = filterproduct;
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            searchTB.Text = string.Empty; // Очищаем текстовое поле поиска
            DGGuest.ItemsSource = products; // Отображаем все товары
        }

    }
}