using System.Windows;

namespace YourNamespace
{
    public partial class StockChartWindow : Window

    {
        public StockChartWindow()
        {
            InitializeComponent();
            DataContext = new StockChartViewModel(); // Replace with your actual view model
        }
    }
    public class StockChartViewModel
    {
        // Your view model code here
    }
}

