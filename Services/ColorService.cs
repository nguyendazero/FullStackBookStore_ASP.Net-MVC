using Microsoft.EntityFrameworkCore;
using DotNet.BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.BookStore.Data;

namespace DotNet.BookStore.Services
{
    public interface IColorService
    {
        List<Color> GetAllColors();

        Color? GetColorById(int id);

        void CreateColor(Color Color);

        void UpdateColor(Color Color);

        void DeleteColor(int id);
    }

    public class ColorService : IColorService
    {

        private readonly DataContext _dataContext;

        public ColorService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void CreateColor(Color Color)
        {
            _dataContext.Colors.Add(Color);
            _dataContext.SaveChanges();
        }

        public void DeleteColor(int id)
        {
            var Color = _dataContext.Colors.FirstOrDefault(c => c.Id == id);
            if (Color == null) return;

            _dataContext.Colors.Remove(Color);
            _dataContext.SaveChanges();
        }

        public List<Color> GetAllColors()
        {
            return _dataContext.Colors
                    .Include(c => c.Laptops)
                    .ToList();
        }

        public Color? GetColorById(int id)
        {
            return _dataContext.Colors
                    .Include(c => c.Laptops)
                    .FirstOrDefault(c => c.Id == id);
        }

        public void UpdateColor(Color color)
        {
            var existColor = _dataContext.Colors.FirstOrDefault(c => c.Id == color.Id);
            if (existColor == null) return;

            existColor.Name = color.Name;

            _dataContext.Colors.Update(existColor);
            _dataContext.SaveChanges();
        }
    }
}

