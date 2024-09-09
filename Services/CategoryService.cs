using Microsoft.EntityFrameworkCore;
using DotNet.BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.BookStore.Data;

namespace DotNet.BookStore.Services
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        Category? GetCategoryById(int id);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Category> GetAllCategories()
        {
            return _dataContext.Categories
                .Include(c => c.Laptops)
                .ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return _dataContext.Categories
                .Include(c => c.Laptops)
                .FirstOrDefault(c => c.Id == id);
        }

        public void CreateCategory(Category category)
        {
            _dataContext.Categories.Add(category);
            _dataContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            var existingCategory = _dataContext.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory == null) return;

            existingCategory.Name = category.Name;
            _dataContext.Categories.Update(existingCategory);
            _dataContext.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _dataContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return;

            _dataContext.Categories.Remove(category);
            _dataContext.SaveChanges();
        }
    }
}
