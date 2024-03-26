using project7.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace project7.Services
{
    public interface IAuthorsService
    {
        Task<List<Author>> GetAuthorsAsync();
        Task<Author> AddAuthorAsync(Author author);
        Task<Author> UpdateAuthorAsync(int id, Author author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
