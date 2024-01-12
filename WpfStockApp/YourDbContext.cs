using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace WpfStockApp
{
    public class MyDbContext
    {
        public DbSet<User> Users { get; set; }

        // Other DbSet properties and DbContext configurations...
    }
}
