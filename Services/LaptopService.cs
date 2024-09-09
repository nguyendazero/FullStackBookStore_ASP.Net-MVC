using Microsoft.EntityFrameworkCore;
using DotNet.LaptopStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.LaptopStore.Data;

namespace DotNet.LaptopStore.Services
{
    public interface ILaptopService
    {
        List<Laptop> GetAllLaptops(string? keySearch = null, int? categoryId = null, int? authorId = null);
        Laptop? GetLaptopById(int id);
        void UpdateLaptop(Laptop Laptop);
        void CreateLaptop(Laptop Laptop);
        void DeleteLaptop(int id);

        List<Laptop> GetLaptopsByCategory(int categoryId);
        List<Laptop> GetLaptopsByColor(int authorId);
        List<Laptop> GetLaptopsByStatus(string status);
        List<Laptop> SearchLaptops(string keySearch);
        List<Laptop> SearchLaptopsByPriceRange(double minPrice, double maxPrice);
    }

    public class LaptopService : ILaptopService
    {
        private readonly DataContext _dataContext;

        public LaptopService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Laptop> GetAllLaptops(string? keySearch = null, int? categoryId = null, int? colorId = null)
        {
            return _dataContext.Laptops
                .Include(l => l.Color)
                .Include(l => l.Category)
                .Where(l => string.IsNullOrEmpty(keySearch) || l.Name.Contains(keySearch) || l.Description.Contains(keySearch))
                .Where(l => !categoryId.HasValue || l.CategoryId == categoryId.Value)
                .Where(l => !colorId.HasValue || l.ColorId == colorId.Value)
                .ToList();
        }

        public Laptop? GetLaptopById(int id)
        {
            return _dataContext.Laptops
                .Include(l => l.Color)
                .Include(l => l.Category)
                .Include(l => l.Ratings)
                .Include(l => l.CartItems)
                .Include(l => l.OrderDetails)
                .FirstOrDefault(l => l.Id == id);
        }

        public void CreateLaptop(Laptop Laptop)
        {
            _dataContext.Laptops.Add(Laptop);
            _dataContext.SaveChanges();
        }

        public void UpdateLaptop(Laptop Laptop)
        {
            var existingLaptop = _dataContext.Laptops.FirstOrDefault(l => l.Id == Laptop.Id);
            if (existingLaptop == null) return;

            existingLaptop.Name = Laptop.Name;
            existingLaptop.Description = Laptop.Description;
            existingLaptop.Image = Laptop.Image;
            existingLaptop.Price = Laptop.Price;
            existingLaptop.Quantity = Laptop.Quantity;
            existingLaptop.Status = Laptop.Status;
            existingLaptop.AverageRating = Laptop.AverageRating;
            existingLaptop.ColorId = Laptop.ColorId;
            existingLaptop.CategoryId = Laptop.CategoryId;

            _dataContext.Laptops.Update(existingLaptop);
            _dataContext.SaveChanges();
        }

        public void DeleteLaptop(int id)
        {
            var Laptop = _dataContext.Laptops.FirstOrDefault(l => l.Id == id);
            if (Laptop == null) return;

            _dataContext.Laptops.Remove(Laptop);
            _dataContext.SaveChanges();
        }

        public List<Laptop> GetLaptopsByCategory(int categoryId)
        {
            return _dataContext.Laptops
                .Include(l => l.Color)
                .Include(l => l.Category)
                .Where(l => l.CategoryId == categoryId)
                .ToList();
        }

        public List<Laptop> GetLaptopsByColor(int colorId)
        {
            return _dataContext.Laptops
                .Include(l => l.Color)
                .Include(l => l.Category)
                .Where(l => l.ColorId == colorId)
                .ToList();
        }

        public List<Laptop> GetLaptopsByStatus(string status)
        {
            return _dataContext.Laptops
                .Include(l => l.Color)
                .Include(l => l.Category)
                .Where(l => l.Status == status)
                .ToList();
        }

        public List<Laptop> SearchLaptops(string keySearch)
        {
            return _dataContext.Laptops
                .Include(l => l.Color)
                .Include(l => l.Category)
                .Where(l => l.Name.Contains(keySearch) || l.Description.Contains(keySearch))
                .ToList();
        }

        public List<Laptop> SearchLaptopsByPriceRange(double minPrice, double maxPrice)
        {
            return _dataContext.Laptops
                .Include(l => l.Color)
                .Include(l => l.Category)
                .Where(l => l.Price >= minPrice && l.Price <= maxPrice)
                .ToList();
        }
    }
}
