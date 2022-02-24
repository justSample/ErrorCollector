using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Main.Model
{
    internal class ErrorCollectorContext : DbContext
    {

        //public DbSet<User> Users { get; set; }

        public ErrorCollectorContext()
        //: base("server=127.0.0.1;uid=root;pwd=root;database=error_collector")
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;user=root;password=root;database=error_collector;");
        }


    }
}
