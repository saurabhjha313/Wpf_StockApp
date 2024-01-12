using Microsoft.EntityFrameworkCore;
using System;

namespace WpfStockApp
{
    // DbContext definition
    public class YourDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public YourDbContext(DbContextOptions<YourDbContext> options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {
            // Your custom logic, if needed, before saving changes
            return base.SaveChanges();
        }

        // Other configurations, methods, or DbSet properties...
    }

    // Interface definition
    public interface IUserService
    {
        User GetUserById(int userId);
        void UpdateUserBalance(int userId, decimal amount);
    }

    // Service implementation
    public class UserService : IUserService
    {
        private readonly YourDbContext _dbContext;

        public UserService(YourDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public User GetUserById(int userId)
        {
            return _dbContext.Users.Find(userId);
        }

        public void UpdateUserBalance(int userId, decimal amount)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.Balance += amount;
                _dbContext.SaveChanges();
            }
        }
    }
}
