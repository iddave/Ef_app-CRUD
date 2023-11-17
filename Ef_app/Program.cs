
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Ef_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            using(AppContext db = new AppContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            AppAdd(options);
            AppRead(options);
            AppRemove(options);
            AppUpdate(options);
            //AppRemoveAll(options);
        }

        public static void AppAdd(DbContextOptions<AppContext> options)
        {
            using (AppContext db = new AppContext(options))
            {
                var customer1 = new Customer
                {
                    Name = "СтройИнвестГрупп",
                    Type = "Крупная строительная компания",
                    ContactInfo = "info@stroyinvestgroup.com"
                };
                var customer2 = new Customer
                {
                    Name = "Городская администрация",
                    Type = "Государственное учреждение",
                    ContactInfo = "city_admin@city.gov"
                };
                db.Customers.AddRange(customer1, customer2);
                db.SaveChanges();

                var contract1 = new Contract
                {
                    Description = "Строительство жилого комплекса \"Модерн\"",
                    Price = 2500000,
                    Customer = customer1
                };
                var contract2 = new Contract
                {
                    Description = "Реконструкция офисного здания \"БизнесЦентр\"",
                    Price = 150000,
                    Customer = customer2
                };
                db.Contracts.AddRange(contract1, contract2);
                db.SaveChanges();

                var management1 = new Management
                {
                    Name = "Отдел строительства жилой недвижимости",
                    Director = "Иванов Алексей Петрович",
                    Contract = contract1
                };
                var management2 = new Management
                {
                    Name = "Департамент инфраструктурных проектов",
                    Director = "Смирнова Елена Владимировна",
                    Contract = contract1
                };
                db.Managements.AddRange(management1, management2);
                db.SaveChanges();
            }
        }

        public static void AppRead(DbContextOptions<AppContext> options)
        {
            using(AppContext db = new AppContext(options))
            {
                var customers = db.Customers.Include(x => x.Contracts).ToList();
                Console.WriteLine("Заказчики:");
                foreach (var c in customers)
                {
                    Console.WriteLine($"{c.Id} {c.Name} {c.Type} {c.ContactInfo}");
                    foreach (var contr in c.Contracts) Console.WriteLine($"Описание заказа - {contr.Description}");
                }

                var contracts = db.Contracts.Include(x => x.Customer).ToList();
                Console.WriteLine("\nЗаказы:");
                foreach (var c in contracts)
                {
                    Console.WriteLine($"{c.Id} {c.Description} - {c.Price},\nИмя заказчика - {c.Customer.Name}");
                    foreach (var m in c.Managements)
                        Console.WriteLine($"Название управление - {m.Name}");
                }

            }
        }

        public static void AppRemove(DbContextOptions<AppContext> options)
        {
            using(AppContext db = new AppContext(options))
            {
                var management = db.Managements.FirstOrDefault();
                if (management != null)
                {
                    db.Managements.Remove(management);
                    db.SaveChanges();  
                }
                foreach (var m in db.Managements.ToList()) Console.WriteLine($"\n{m.Id} {m.Name}");
            }
        }

        public static void AppUpdate(DbContextOptions<AppContext> options)
        {
            using(AppContext db = new AppContext(options))
            {
                var customer = db.Customers.FirstOrDefault();
                if (customer != null)
                {
                    customer.Name += "EDITED";
                    customer.Contracts.Add(new Contract {Description = "Some new contract", Price = 10 });
                    db.SaveChanges();
                }
            }
        }

        public static void AppRemoveAll(DbContextOptions<AppContext> options)
        {
            using(AppContext db = new AppContext(options))
            {
                var allCustomers = db.Customers.ToList();
                var allManagements = db.Managements.ToList();
                var allContracts = db.Contracts.ToList();
                db.Customers.RemoveRange(allCustomers);
                db.Contracts.RemoveRange(allContracts);
                db.Managements.RemoveRange(allManagements);
                db.SaveChanges();
            }
        }
    }


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
        public AppContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();

            // получаем конфигурацию из файла appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            // получаем строку подключения из файла appsettings.json
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new AppContext(optionsBuilder.Options);
        }
    }
}