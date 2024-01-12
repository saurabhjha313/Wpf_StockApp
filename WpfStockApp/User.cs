using Microsoft.EntityFrameworkCore;

namespace WpfStockApp
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public decimal Balance { get; set; }
        // Additional properties like portfolio, etc.
    }
}
