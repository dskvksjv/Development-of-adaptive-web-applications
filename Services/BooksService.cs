using project7.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project7.Services
{
    public class BooksService : IBooksService
    {
        private readonly List<Book> _books = new List<Book>();

        public BooksService()
        {
            _books.Add(new Book { Id = 1, Title = "Гетьманщина", Author = "Іван Нечуй-Левицький", Year = 1883 });
            _books.Add(new Book { Id = 2, Title = "Тигролови", Author = "Іван Багряний", Year = 1926 });
            _books.Add(new Book { Id = 3, Title = "Кайдашева сім'я", Author = "Іван Нечуй-Левицький", Year = 1899 });
            _books.Add(new Book { Id = 4, Title = "Маруся", Author = "Борис Грінченко", Year = 1883 });
            _books.Add(new Book { Id = 5, Title = "Тарас Бульба", Author = "Микола Гоголь", Year = 1835 });
            _books.Add(new Book { Id = 6, Title = "Кобзар", Author = "Тарас Шевченко", Year = 1840 });
            _books.Add(new Book { Id = 7, Title = "Запорожець за Дунаєм", Author = "Іван Нечуй-Левицький", Year = 1883 });
            _books.Add(new Book { Id = 8, Title = "Сонячний удар", Author = "Михайло Коцюбинський", Year = 1898 });
            _books.Add(new Book { Id = 9, Title = "Чорна рада", Author = "Василь Короленко", Year = 1900 });
            _books.Add(new Book { Id = 10, Title = "Аскольдова могила", Author = "Іван Нечуй-Левицький", Year = 1885 });
        }

        public Task<List<Book>> GetBooksAsync()
        {
            return Task.FromResult(_books);
        }

        public Task<Book> AddBookAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            book.Id = _books.Count + 1;
            _books.Add(book);
            return Task.FromResult(book);
        }

        public Task<Book> UpdateBookAsync(int id, Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Year = book.Year;
                return Task.FromResult(existingBook);
            }
            return Task.FromResult<Book>(null);
        }

        public Task<bool> DeleteBookAsync(int id)
        {
            var bookToRemove = _books.FirstOrDefault(b => b.Id == id);
            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
