using Microsoft.EntityFrameworkCore;
using DotNet.BookStore.Models;
using System.Collections.Generic;
using DotNet.BookStore.Data;
using System.Linq;

namespace DotNet.BookStore.Services
{
    public interface IAuthorService
    {
        List<Author> GetAllAuthors(string? keySearch = null, int? bookId = null);
        Author? GetAuthorById(int id);
        void UpdateAuthor(Author author);
        void CreateAuthor(Author author);
        void DeleteAuthor(int id);
    }

    public class AuthorService : IAuthorService
    {
        private readonly DataContext _dataContext;

        public AuthorService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Author> GetAllAuthors(string? keySearch = null, int? bookId = null)
        {
            return _dataContext.Authors
                .Include(a => a.Books)
                .Where(a => string.IsNullOrEmpty(keySearch) || a.FullName.Contains(keySearch) || a.Story.Contains(keySearch))
                .Where(a => !bookId.HasValue || a.Books.Any(b => b.Id == bookId.Value))
                .ToList();
        }

        public Author? GetAuthorById(int id)
        {
            return _dataContext.Authors
                .Include(a => a.Books)
                .FirstOrDefault(a => a.Id == id);
        }

        public void CreateAuthor(Author author)
        {
            _dataContext.Authors.Add(author);
            _dataContext.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            var existingAuthor = _dataContext.Authors.FirstOrDefault(a => a.Id == author.Id);
            if (existingAuthor == null) return;

            existingAuthor.FullName = author.FullName;
            existingAuthor.HomeTown = author.HomeTown;
            existingAuthor.Story = author.Story;
            existingAuthor.YearOfBirth = author.YearOfBirth;

            _dataContext.Authors.Update(existingAuthor);
            _dataContext.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            var author = _dataContext.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return;

            _dataContext.Authors.Remove(author);
            _dataContext.SaveChanges();
        }
    }
}
