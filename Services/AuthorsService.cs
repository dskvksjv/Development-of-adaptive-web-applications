using project7.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project7.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly List<Author> _authors = new List<Author>();

        public AuthorsService()
        {
            _authors.Add(new Author { Id = 1, Name = "Тарас Шевченко" });
            _authors.Add(new Author { Id = 2, Name = "Леся Українка" });
            _authors.Add(new Author { Id = 3, Name = "Іван Франко" });
            _authors.Add(new Author { Id = 4, Name = "Микола Гоголь" });
            _authors.Add(new Author { Id = 5, Name = "Іван Котляревський" });
            _authors.Add(new Author { Id = 6, Name = "Леонід Глібов" });
            _authors.Add(new Author { Id = 7, Name = "Олександр Довженко" });
            _authors.Add(new Author { Id = 8, Name = "Василь Стефаник" });
            _authors.Add(new Author { Id = 9, Name = "Марко Вовчок" });
            _authors.Add(new Author { Id = 10, Name = "Олесь Гончар" });
        }

        public Task<List<Author>> GetAuthorsAsync()
        {
            return Task.FromResult(_authors);
        }

        public Task<Author> AddAuthorAsync(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            author.Id = _authors.Count + 1;
            _authors.Add(author);
            return Task.FromResult(author);
        }

        public Task<Author> UpdateAuthorAsync(int id, Author author)
        {
            var existingAuthor = _authors.FirstOrDefault(a => a.Id == id);
            if (existingAuthor != null)
            {
                existingAuthor.Name = author.Name;
                return Task.FromResult(existingAuthor);
            }
            return Task.FromResult<Author>(null);
        }

        public Task<bool> DeleteAuthorAsync(int id)
        {
            var authorToRemove = _authors.FirstOrDefault(a => a.Id == id);
            if (authorToRemove != null)
            {
                _authors.Remove(authorToRemove);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
