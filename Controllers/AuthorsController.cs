using Microsoft.AspNetCore.Mvc;
using project7.Model;
using project7.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace project7.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAuthors()
        {
            var authors = await _authorsService.GetAuthorsAsync();
            return Ok(new ResponseModel<List<Author>> { Data = authors, StatusCode = 200, Message = "Success" });
        }

        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthor(Author author)
        {
            var newAuthor = await _authorsService.AddAuthorAsync(author);
            return Ok(new ResponseModel<Author> { Data = newAuthor, StatusCode = 200, Message = "Author added successfully" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> UpdateAuthor(int id, Author author)
        {
            var updatedAuthor = await _authorsService.UpdateAuthorAsync(id, author);
            if (updatedAuthor == null)
            {
                return NotFound(new ResponseModel<Author> { StatusCode = 404, Message = "Author not found" });
            }
            return Ok(new ResponseModel<Author> { Data = updatedAuthor, StatusCode = 200, Message = "Author updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var result = await _authorsService.DeleteAuthorAsync(id);
            if (!result)
            {
                return NotFound(new ResponseModel<object> { StatusCode = 404, Message = "Author not found" });
            }
            return Ok(new ResponseModel<object> { StatusCode = 200, Message = "Author deleted successfully" });
        }
    }
}