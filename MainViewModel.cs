using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using POS.DAL;
using POS.Models;

namespace POS
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Customer> _customers;
        private ObservableCollection<Order> _orders;
        private ObservableCollection<Product> _products;

        private Customer _selectedCustomer;
        private Order _selectedOrder;

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set { _customers = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set { _orders = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Product> Products
        {
            get => _products;
            set { _products = value; OnPropertyChanged(); }
        }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                LoadOrdersForCustomer();
                OnPropertyChanged();
            }
        }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                LoadProductsForOrder();
                OnPropertyChanged();
            }
        }

        public decimal TotalBillAmount { get; set; }

        public MainViewModel()
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            // Load data from the database (this is a simplified version)
            Customers = new ObservableCollection<Customer>(DatabaseHelper.GetCustomers());
        }

        private void LoadOrdersForCustomer()
        {
            if (SelectedCustomer != null)
            {
                Orders = new ObservableCollection<Order>(DatabaseHelper.GetOrders(SelectedCustomer.CustomerID));
                ShowOrdersWindow();
            }
        }

        private void LoadProductsForOrder()
        {
            if (SelectedOrder != null)
            {
                Products = new ObservableCollection<Product>(DatabaseHelper.GetProducts(SelectedOrder.OrderID));
                TotalBillAmount = Products.Sum(p => p.Price * p.Quantity);

                ShowProductsWindow();
            }
        }

        private void ShowOrdersWindow()
        {
            // Create the OrdersWindow and pass the data
            OrdersWindow ordersWindow = new OrdersWindow
            {
                DataContext = this  // Pass the current ViewModel or a new ViewModel
            };
            ordersWindow.ShowDialog();  // Use ShowDialog() to make it modal
        }

        private void ShowProductsWindow()
        {
            // Assuming you have another ProductsWindow
            ProductsWindow productsWindow = new ProductsWindow
            {
                DataContext = this  // Pass the current ViewModel or a new ViewModel for products
            };
            productsWindow.ShowDialog();  // Show modal window for products
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
