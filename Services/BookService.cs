using Microsoft.EntityFrameworkCore;
using DotNet.BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.BookStore.Data;

namespace DotNet.BookStore.Services
{
    public interface IBookService
    {
        List<Book> GetAllBooks(string? keySearch = null, int? categoryId = null, int? authorId = null);
        Book? GetBookById(int id);
        void UpdateBook(Book book);
        void CreateBook(Book book);
        void DeleteBook(int id);

        List<Book> GetBooksByCategory(int categoryId);
        List<Book> GetBooksByAuthor(int authorId);
        List<Book> GetBooksByStatus(string status);
        List<Book> SearchBooks(string keySearch);
        List<Book> SearchBooksByPriceRange(double minPrice, double maxPrice);
    }

    public class BookService : IBookService
    {
        private readonly DataContext _dataContext;

        public BookService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Book> GetAllBooks(string? keySearch = null, int? categoryId = null, int? authorId = null)
        {
            return _dataContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => string.IsNullOrEmpty(keySearch) || b.Name.Contains(keySearch) || b.Description.Contains(keySearch))
                .Where(b => !categoryId.HasValue || b.CategoryId == categoryId.Value)
                .Where(b => !authorId.HasValue || b.AuthorId == authorId.Value)
                .ToList();
        }

        public Book? GetBookById(int id)
        {
            return _dataContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Ratings)
                .Include(b => b.CartItems)
                .Include(b => b.OrderDetails)
                .Include(b => b.LikeRatings)
                .FirstOrDefault(b => b.Id == id);
        }

        public void CreateBook(Book book)
        {
            _dataContext.Books.Add(book);
            _dataContext.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            var existingBook = _dataContext.Books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook == null) return;

            existingBook.Name = book.Name;
            existingBook.Description = book.Description;
            existingBook.Image = book.Image;
            existingBook.Price = book.Price;
            existingBook.Quantity = book.Quantity;
            existingBook.Status = book.Status;
            existingBook.AverageStars = book.AverageStars;
            existingBook.AuthorId = book.AuthorId;
            existingBook.CategoryId = book.CategoryId;

            _dataContext.Books.Update(existingBook);
            _dataContext.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = _dataContext.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return;

            _dataContext.Books.Remove(book);
            _dataContext.SaveChanges();
        }

        public List<Book> GetBooksByCategory(int categoryId)
        {
            return _dataContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.CategoryId == categoryId)
                .ToList();
        }

        public List<Book> GetBooksByAuthor(int authorId)
        {
            return _dataContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.AuthorId == authorId)
                .ToList();
        }

        public List<Book> GetBooksByStatus(string status)
        {
            return _dataContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.Status == status)
                .ToList();
        }

        public List<Book> SearchBooks(string keySearch)
        {
            return _dataContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.Name.Contains(keySearch) || b.Description.Contains(keySearch))
                .ToList();
        }

        public List<Book> SearchBooksByPriceRange(double minPrice, double maxPrice)
        {
            return _dataContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.Price >= minPrice && b.Price <= maxPrice)
                .ToList();
        }
    }
}
