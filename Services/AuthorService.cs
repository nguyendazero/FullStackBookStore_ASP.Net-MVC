using Microsoft.EntityFrameworkCore;
using DotNet.BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.BookStore.Data;

namespace DotNet.BookStore.Services
{
    public interface IAuthorService
    {
        List<Author> GetAllAuthors();

        Author? GetAuthorById(int id);

        void CreateAuthor(Author author);

        void UpdateAuthor(Author author);

        void DeleteAuthor(int id);
    }

    public class AuthorService : IAuthorService
    {

        private readonly DataContext _dataContext;

        public AuthorService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void CreateAuthor(Author author)
        {
            _dataContext.Authors.Add(author);
            _dataContext.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            var author = _dataContext.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return;

            _dataContext.Authors.Remove(author);
            _dataContext.SaveChanges();
        }

        public List<Author> GetAllAuthors()
        {
            return _dataContext.Authors
                    .Include(a => a.Books)
                    .ToList();
        }

        public Author? GetAuthorById(int id)
        {
            return _dataContext.Authors
                    .Include(a => a.Books)
                    .FirstOrDefault(a => a.Id == id);
        }

        public void UpdateAuthor(Author author)
        {
            var existAuthor = _dataContext.Authors.FirstOrDefault(a => a.Id == author.Id);
            if (existAuthor == null) return;

            existAuthor.YearOfBirth = author.YearOfBirth;
            existAuthor.FullName = author.FullName;
            existAuthor.Story = author.Story;
            existAuthor.HomeTown = author.HomeTown;

            _dataContext.Authors.Update(existAuthor);
            _dataContext.SaveChanges();
        }
    }
}

