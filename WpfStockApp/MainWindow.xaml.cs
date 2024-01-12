using System;
using System.Windows;
using WpfStockApp;
using NLog;
using Microsoft.EntityFrameworkCore;

namespace StockTradingApp
{
    public partial class MainWindow : Window
    {
        private StockViewModel stockViewModel;
        private UserViewModel userViewModel;

        public MainWindow()
        {
            InitializeComponent();
            stockViewModel = new StockViewModel();
            YourDbContext dbContext = new YourDbContext(new DbContextOptions<YourDbContext>());
            IUserService userService = new UserService(dbContext);
            userViewModel = new UserViewModel(userService); // Initialize the UserViewModel with the UserService
            DataContext = stockViewModel; // Default DataContext to StockViewModel
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic to restart the application
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await stockViewModel.ConnectToWebSocketAsync();
                userViewModel.LoadUser(1); // Load user with ID 1
                DataContext = userViewModel; // Set DataContext to UserViewModel after loading user
            }
            catch (Exception ex)
            {
                // Log the exception and show a message to the user
                LogManager.GetCurrentClassLogger().Error(ex, "Failed to load user data or connect to WebSocket.");
                MessageBox.Show("An error occurred while loading user data or connecting to WebSocket.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteTradeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                stockViewModel.ExecuteTrade(); // Call a method in your StockViewModel
            }
            catch (Exception ex)
            {
                // Log the exception and show a message to the user
                LogManager.GetCurrentClassLogger().Error(ex, "Trade execution failed.");
                MessageBox.Show("An error occurred during trade execution.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
