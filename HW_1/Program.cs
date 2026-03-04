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
                var shop = new Shop(context);

             
                Console.WriteLine("\n=== Робота з категоріями ===");
                shop.ShowAllCategories();

                shop.CreateCategory("Одяг");
                shop.CreateCategory("Взуття");

                shop.ShowAllCategories();

                shop.UpdateCategoryName(3, "Іграшки та розваги");

                shop.ShowAllCategories();

                // shop.DeleteCategory(4);

                Console.WriteLine("\n=== Робота з продуктами ===");
                shop.ShowAllProducts();

                shop.CreateProduct("Футболка", "Бавовна, розмір M", 499m, 12, 3);
                shop.CreateProduct("Кросівки Nike", "Білий колір", 3499m, 8, 4);

                shop.ShowAllProducts();

                shop.UpdateProductName(1, "Смартфон Samsung оновлений");
                shop.UpdateProductQuantity(1, 5); 

                shop.ShowAllProducts();

                // shop.DeleteProduct(10);


                shop.ShowCategoryByProductId(1);   
                shop.ShowCategoryByProductId(5);  


                shop.ShowProductsByCategoryId(1);
                shop.ShowProductsByCategoryId(3);
                shop.ShowProductsByCategoryId(999);
            }

            Console.WriteLine("\nПрограма завершена. Натисніть Enter для виходу...");
            Console.ReadLine();
        }

    
        static void AddTestDataIfNeeded(Shop shop, ShopContext context)
        {
            if (context.Categories.Any())
            {
                return; 
            }

            Console.WriteLine("Додаємо початкові тестові дані...");

            shop.CreateCategory("Електроніка");
            shop.CreateCategory("Книги");

            int electronicsId = context.Categories.First(c => c.Name == "Електроніка").Id;

            shop.CreateProduct("Смартфон", "Новий телефон", 15000m, 0, electronicsId);
            shop.CreateProduct("Планшет", "10 дюймів", 12000m, 2, electronicsId);
            shop.CreateProduct("Книга про EF Core", "Посібник", 450m, 20, context.Categories.First(c => c.Name == "Книги").Id);
            shop.CreateProduct("Монітор 27\"", "Full HD", 6500m, 0, electronicsId);

            Console.WriteLine("Тестові дані додані.\n");
        }
    }
}