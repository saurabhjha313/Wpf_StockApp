using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts.Wpf;
using LiveCharts;

public class MainViewModel : ViewModelBase, INotifyPropertyChanged
{
    private readonly StockDataService _stockDataService;
    private ObservableCollection<StockDataPoint> _stockData;

    public MainViewModel()
    {
        _stockDataService = new StockDataService("II5OJUI4U61R0CHU");
        FetchDataCommand = new RelayCommand(async () => await FetchData());
    }

    public ObservableCollection<StockDataPoint> StockData
    {
        get => _stockData;
        set
        {
            _stockData = value;
            RaisePropertyChanged();
        }
    }

    public SeriesCollection StockSeries { get; set; }
    public Func<double, string> DateFormatter { get; set; }

    public RelayCommand FetchDataCommand { get; }

    private async Task FetchData()
    {
        var data = await _stockDataService.GetHistoricalDataAsync("AAPL", "5min");
        StockData = new ObservableCollection<StockDataPoint>(data);

        StockSeries = new SeriesCollection
        {
            new LineSeries
            {
                Values = new ChartValues<StockDataPoint>(StockData),
                PointGeometry = null // This removes the point markers
            }
        };

        DateFormatter = value => new DateTime((long)value).ToString("t");

        OnPropertyChanged(nameof(StockSeries));
        OnPropertyChanged(nameof(DateFormatter));
    }

    public new event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
