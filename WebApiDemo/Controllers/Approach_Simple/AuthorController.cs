using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebApiDemo.DTO;
using WebApiDemo.Models;
using WebApiDemo.Services.Approach_Simple;

namespace WebApiDemo.Controllers.Approach_Simple
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _authorService.getAllAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.getAsync(id);
            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorDTO model)
        {
            try
            {
                var newAuthorId = await _authorService.CreateAsync(model);
                var author = await _authorService.getAsync(newAuthorId);
                return author == null ? NotFound() : Ok(author);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AuthorDTO model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            await _authorService.UpdateAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAsync(id);
            return Ok();
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search(string txtSearch)
        {
            Expression<Func<Author, bool>> filter = a => a.AuFname!.Contains(txtSearch);
            var entityList = await _authorService.SearchAsync(filter, true);
            if (entityList.Any())
            {
                return Ok(entityList);
            }
            return NoContent();
        }
    }
}
