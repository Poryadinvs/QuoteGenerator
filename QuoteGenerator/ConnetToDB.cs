using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteGenerator
{
    public class Client
    {
        public int ID { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
    }

    public class AppConect : DbContext
    {
        public DbSet<Client> client => Set<Client>();

        public AppConect() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=client.db");
        }
    }
}
