using System.ComponentModel;
using WpfStockApp;

public class UserViewModel : INotifyPropertyChanged
{
    private readonly IUserService _userService;
    private User _user;

    public UserViewModel(IUserService userService)
    {
        _userService = userService;
    }

    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged(nameof(User));
        }
    }

    public void LoadUser(int userId)
    {
        User = _userService.GetUserById(userId);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
