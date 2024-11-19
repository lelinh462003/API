using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebApiDemo.DTO;
using WebApiDemo.Models;
using WebApiDemo.Services.Second_Approach;

namespace WebApiDemo.Controllers.Second_Approach
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private readonly IGenericService<Title, TitleDTO> _repo;

        public TitleController(IGenericService<Title, TitleDTO> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<TitleDTO>>> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(items);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TitleDTO>> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TitleDTO model)
        {
            var a = await _repo.PostAsync(model);
            var A = await _repo.GetByIdAsync(a);
            return Ok(A);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, TitleDTO model)
        {
            if (id != model.Id)
                return BadRequest();
            await _repo.PutAsync(id, model);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var a = await _repo.GetByIdAsync(id);
            if (a == null)
                return NotFound();

            await _repo.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TitleDTO>>> Search(string txtSearch)
        {
            Expression<Func<Title, bool>> filter = c => c.Status != -1 && c.Code.Contains(txtSearch);
            var entityList = await _repo.SearchAsync(filter);
            if (entityList != null && entityList.Any())
            {
                return Ok(entityList);
            }
            return NoContent();
        }
    }
}
