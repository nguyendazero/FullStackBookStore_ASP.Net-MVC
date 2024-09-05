using Microsoft.EntityFrameworkCore;
using DotNet.BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.BookStore.Data;

namespace DotNet.BookStore.Services
{
    public interface ILikeRatingService
    {
        List<LikeRating> GetAllLikeRatings();
        LikeRating? GetLikeRatingById(int id);
        void CreateLikeRating(LikeRating likeRating);
        void UpdateLikeRating(LikeRating likeRating);
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
                .Include(lr => lr.Book)
                .Include(lr => lr.User)
                .Include(lr => lr.Rating)
                .ToList();
        }

        public LikeRating? GetLikeRatingById(int id)
        {
            return _dataContext.LikeRatings
                .Include(lr => lr.Book)
                .Include(lr => lr.User)
                .Include(lr => lr.Rating)
                .FirstOrDefault(lr => lr.Id == id);
        }

        public void CreateLikeRating(LikeRating likeRating)
        {
            _dataContext.LikeRatings.Add(likeRating);
            _dataContext.SaveChanges();
        }

        public void UpdateLikeRating(LikeRating likeRating)
        {
            var existingLikeRating = _dataContext.LikeRatings
                .FirstOrDefault(lr => lr.Id == likeRating.Id);
            if (existingLikeRating == null) return;

            existingLikeRating.BookId = likeRating.BookId;
            existingLikeRating.UserId = likeRating.UserId;
            existingLikeRating.RatingId = likeRating.RatingId;

            _dataContext.LikeRatings.Update(existingLikeRating);
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
