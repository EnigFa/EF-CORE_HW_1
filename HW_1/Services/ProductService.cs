using HW_1.Data;
using HW_1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace HW_1.Services
{
    public class ProductService
    {
        private readonly ShopContext _context;

        public ProductService(ShopContext context)
        {
            _context = context;
        }

        public void CreateProduct(string name, string description, decimal price, int quantity, int categoryId)
        {
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

            Console.WriteLine($"Створено товар: {product.Name} (ID = {product.Id})");
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

            Console.WriteLine($"Назва змінена на: {newName}");
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

            Console.WriteLine($"Кількість змінена на: {newQuantity}");
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

        public void ShowNoStockProducts()
        {
            var noStock = _context.Products
                .Where(p => p.StockQuantity == 0)
                .ToList();

            Console.WriteLine("\nТовари, яких немає в наявності:");
            if (noStock.Count == 0)
            {
                Console.WriteLine("Таких товарів немає.");
                return;
            }

            foreach (var p in noStock)
            {
                Console.WriteLine($"{p.Id} | {p.Name} | {p.Price} грн");
            }
        }

        public void ShowTop3MostExpensive()
        {
            var top = _context.Products
                .OrderByDescending(p => p.Price)
                .Take(3)
                .ToList();

            Console.WriteLine("\nТоп-3 найдорожчих товарів:");
            foreach (var p in top)
            {
                Console.WriteLine($"{p.Id} | {p.Name} | {p.Price} грн");
            }
        }
    }
}
