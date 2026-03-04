using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HW_1.Data;
using HW_1.Services;
using System;

namespace HW_1
{
    class Program
    {
        static IConfiguration configuration;

        static void Main(string[] args)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();


            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();
            string connString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connString);

            using (var context = new ShopContext(optionsBuilder.Options))
            {
  
                var productService = new ProductService(context);

        
                AddTestData(context);

            
                productService.CreateProduct("Клавіатура механічна", "RGB підсвітка", 2200.0m, 7, 1);
                productService.UpdateProductName(1, "Телефон оновлений");
                productService.UpdateProductQuantity(1, 4);
                productService.DeleteProduct(100);
                productService.ShowNoStockProducts();
                productService.ShowTop3MostExpensive();
            }

            Console.WriteLine("\nПрограма завершена. Натисни Enter...");
            Console.ReadLine();
        }

        static void AddTestData(ShopContext context)
        {
            if (context.Categories.Any())
            {
                return; 
            }

            var cat1 = new Models.Category { Name = "Електроніка" };
            var cat2 = new Models.Category { Name = "Книги" };
            context.Categories.Add(cat1);
            context.Categories.Add(cat2);
            context.SaveChanges();

            context.Products.Add(new Models.Product { Name = "Смартфон", Description = "Новий телефон", Price = 15000m, StockQuantity = 0, CategoryId = cat1.Id });
            context.Products.Add(new Models.Product { Name = "Планшет", Description = "10 дюймів", Price = 12000m, StockQuantity = 2, CategoryId = cat1.Id });
            context.Products.Add(new Models.Product { Name = "Книга про EF Core", Description = "Посібник", Price = 450m, StockQuantity = 20, CategoryId = cat2.Id });
            context.Products.Add(new Models.Product { Name = "Монітор 27\"", Description = "Full HD", Price = 6500m, StockQuantity = 0, CategoryId = cat1.Id });
            context.SaveChanges();

            Console.WriteLine("Додано тестові категорії та товари.");
        }
    }
}