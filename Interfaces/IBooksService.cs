using project7.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace project7.Services
{
    public interface IBooksService
    {
        Task<List<Book>> GetBooksAsync();
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(int id, Book book);
        Task<bool> DeleteBookAsync(int id);
    }
}
