using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_app.repository.implementation
{
    
    internal class AppContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Management> Managements { get; set; }

        //public AppContext() { } 

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

    }

    internal class SampleContextFactory : IDesignTimeDbContextFactory<AppContext>
    {
        public const string configFile = "appsettings.json";
        public AppContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();

            // получаем конфигурацию из файла appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(configFile);
            IConfigurationRoot config = builder.Build();

            // получаем строку подключения из файла appsettings.json
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new AppContext(optionsBuilder.Options);
        }
    }
}
