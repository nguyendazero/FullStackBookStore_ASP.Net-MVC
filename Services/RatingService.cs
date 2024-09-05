using Microsoft.EntityFrameworkCore;
using DotNet.BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.BookStore.Data;

namespace DotNet.BookStore.Services
{
    public interface IRatingService
    {
        Rating SaveRating(Rating rating);
        Rating? GetRatingById(int id);
        Rating? UpdateRating(Rating rating);
        void DeleteRating(int id);
        List<Rating> GetRatingsByBookId(int bookId);
        double CalculateAverageStars(int bookId);
    }

    public class RatingService : IRatingService
    {
        private readonly DataContext _dataContext;

        public RatingService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Rating SaveRating(Rating rating)
        {
            _dataContext.Ratings.Add(rating);
            _dataContext.SaveChanges();
            return rating;
        }

        public Rating? GetRatingById(int id)
        {
            return _dataContext.Ratings
                .Include(r => r.Book)
                .Include(r => r.User)
                .FirstOrDefault(r => r.Id == id);
        }


        public Rating? UpdateRating(Rating rating)
        {
            var existingRating = _dataContext.Ratings
                .FirstOrDefault(r => r.Id == rating.Id);

            if (existingRating != null)
            {
                existingRating.Stars = rating.Stars;
                existingRating.Content = rating.Content;
                existingRating.Date = rating.Date;
                existingRating.BookId = rating.BookId;
                existingRating.UserId = rating.UserId;

                _dataContext.Ratings.Update(existingRating);
                _dataContext.SaveChanges();
            }

            return existingRating;
        }


        public void DeleteRating(int id)
        {
            var rating = _dataContext.Ratings.Find(id);
            if (rating != null)
            {
                _dataContext.Ratings.Remove(rating);
                _dataContext.SaveChanges();
            }
        }

        public List<Rating> GetRatingsByBookId(int bookId)
        {
            return _dataContext.Ratings
                .Where(r => r.BookId == bookId)
                .ToList();
        }

        public double CalculateAverageStars(int bookId)
        {
            var ratings = _dataContext.Ratings
                .Where(r => r.BookId == bookId)
                .ToList();

            if (ratings.Count == 0)
            {
                return 0;
            }

            return ratings.Average(r => r.Stars);
        }
    }
}
