using Microsoft.EntityFrameworkCore;
using DotNet.LaptopStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.LaptopStore.Data;

namespace DotNet.LaptopStore.Services
{
    public interface ILikeRatingService
    {
        List<LikeRating> GetAllLikeRatings();
        LikeRating? GetLikeRatingById(int id);
        void CreateLikeRating(LikeRating likeRating);
        void DeleteLikeRating(int id);
    }

    public class LikeRatingService : ILikeRatingService
    {
        private readonly DataContext _dataContext;

        public LikeRatingService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<LikeRating> GetAllLikeRatings()
        {
            return _dataContext.LikeRatings
                .Include(lr => lr.Laptop)
                .Include(lr => lr.User)
                .Include(lr => lr.Rating)
                .ToList();
        }

        public LikeRating? GetLikeRatingById(int id)
        {
            return _dataContext.LikeRatings
                .Include(lr => lr.Laptop)
                .Include(lr => lr.User)
                .Include(lr => lr.Rating)
                .FirstOrDefault(lr => lr.Id == id);
        }

        public void CreateLikeRating(LikeRating likeRating)
        {
            _dataContext.LikeRatings.Add(likeRating);
            _dataContext.SaveChanges();
        }

        public void DeleteLikeRating(int id)
        {
            var likeRating = _dataContext.LikeRatings
                .FirstOrDefault(lr => lr.Id == id);
            if (likeRating == null) return;

            _dataContext.LikeRatings.Remove(likeRating);
            _dataContext.SaveChanges();
        }
    }
}
