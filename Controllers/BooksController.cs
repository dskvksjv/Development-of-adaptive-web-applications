using Microsoft.AspNetCore.Mvc;
using project7.Model;
using project7.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace project7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _booksService.GetBooksAsync();
            return Ok(new ResponseModel<List<Book>> { Data = books, StatusCode = 200, Message = "Success" });
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            var newBook = await _booksService.AddBookAsync(book);
            return Ok(new ResponseModel<Book> { Data = newBook, StatusCode = 200, Message = "Book added successfully" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, Book book)
        {
            var updatedBook = await _booksService.UpdateBookAsync(id, book);
            if (updatedBook == null)
            {
                return NotFound(new ResponseModel<Book> { StatusCode = 404, Message = "Book not found" });
            }
            return Ok(new ResponseModel<Book> { Data = updatedBook, StatusCode = 200, Message = "Book updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var result = await _booksService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound(new ResponseModel<object> { StatusCode = 404, Message = "Book not found" });
            }
            return Ok(new ResponseModel<object> { StatusCode = 200, Message = "Book deleted successfully" });
        }
    }
}
