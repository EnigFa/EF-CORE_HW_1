using HW_1.Data;
using HW_1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HW_1.Services
{
    public class Shop
    {
        private readonly ShopContext _context;

        public Shop(ShopContext context)
        {
            _context = context;
        }

  

        public void CreateCategory(string name)
        {
            var category = new Category { Name = name };
            _context.Categories.Add(category);
            _context.SaveChanges();
            Console.WriteLine($"Створено категорію: {name} (ID = {category.Id})");
        }

        public void ShowAllCategories()
        {
            var categories = _context.Categories.ToList();
            Console.WriteLine("\nСписок усіх категорій:");
            if (categories.Count == 0)
            {
                Console.WriteLine("Категорій ще немає.");
                return;
            }
            foreach (var cat in categories)
            {
                Console.WriteLine($"{cat.Id} | {cat.Name}");
            }
        }

        public void UpdateCategoryName(int id, string newName)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                Console.WriteLine($"Категорію з ID {id} не знайдено");
                return;
            }
            category.Name = newName;
            _context.SaveChanges();
            Console.WriteLine($"Назву категорії змінено на: {newName}");
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                Console.WriteLine($"Категорію з ID {id} не знайдено");
                return;
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            Console.WriteLine($"Категорію {category.Name} видалено");
        }



        public void CreateProduct(string name, string description, decimal price, int quantity, int categoryId)
        {
            if (!_context.Categories.Any(c => c.Id == categoryId))
            {
                Console.WriteLine($"Категорії з ID {categoryId} не існує!");
                return;
            }

            var product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = quantity,
                CategoryId = categoryId
            };

            _context.Products.Add(product);
            _context.SaveChanges();
            Console.WriteLine($"Створено товар: {name} (ID = {product.Id})");
        }

        public void ShowAllProducts()
        {
            var products = _context.Products.ToList();
            Console.WriteLine("\nСписок усіх товарів:");
            if (products.Count == 0)
            {
                Console.WriteLine("Товарів ще немає.");
                return;
            }
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id} | {p.Name} | {p.Price} грн | Кількість: {p.StockQuantity} | Категорія ID: {p.CategoryId}");
            }
        }

        public void UpdateProductName(int id, string newName)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine($"Товар з ID {id} не знайдено");
                return;
            }
            product.Name = newName;
            _context.SaveChanges();
            Console.WriteLine($"Назва товару змінена на: {newName}");
        }

        public void UpdateProductQuantity(int id, int newQuantity)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine($"Товар з ID {id} не знайдено");
                return;
            }
            product.StockQuantity = newQuantity;
            _context.SaveChanges();
            Console.WriteLine($"Кількість товару змінена на: {newQuantity}");
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine($"Товар з ID {id} не знайдено");
                return;
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            Console.WriteLine($"Товар {product.Name} видалено");
        }


        public void ShowCategoryByProductId(int productId)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                Console.WriteLine($"Товар з ID {productId} не знайдено");
                return;
            }

            if (product.Category == null)
            {
                Console.WriteLine($"У товару {product.Name} немає категорії");
                return;
            }

            Console.WriteLine($"\nТовар \"{product.Name}\" належить до категорії: {product.Category.Name} (ID {product.Category.Id})");
        }

        public void ShowProductsByCategoryId(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
            {
                Console.WriteLine($"Категорії з ID {categoryId} не знайдено");
                return;
            }

            var products = _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            Console.WriteLine($"\nТовари в категорії \"{category.Name}\":");
            if (products.Count == 0)
            {
                Console.WriteLine("У цій категорії немає товарів.");
                return;
            }

            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id} | {p.Name} | {p.Price} грн | Кількість: {p.StockQuantity}");
            }
        }
    }
}