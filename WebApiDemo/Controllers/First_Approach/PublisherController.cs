using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebApiDemo.DTO;
using WebApiDemo.Models;
using WebApiDemo.Services.First_Approach;

namespace WebApiDemo.Controllers.First_Approach
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        //First approach (way 1) about Repository Patten in Powerpoint file
        private readonly IService<Publisher> _publisherService;
        private readonly IMapper _mapper;
        public PublisherController(IService<Publisher> publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDTO>> Get(int id)
        {
            var entity = await _publisherService.GetAsync(id);
            if (entity != null)
            {
                var model = new PublisherDTO();
                _mapper.Map(entity, model);
                return Ok(model);
            }
            return NotFound();
        }

        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<PublisherDTO>>> GetList()
        {
            var entityList = await _publisherService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<PublisherDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _publisherService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PublisherDTO>> Create(PublisherDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            var maxId = await _publisherService.MaxIdAsync(model.Id);
            model.Id = maxId + 1;

            // Map data model --> newModel
            var newModel = new Publisher();
            _mapper.Map(model, newModel);
            // newModel.CreatedAt = DateTime.Now;
            if (await _publisherService.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<PublisherDTO>> Update(PublisherDTO model)
        {
            if (_publisherService.CheckExists(model.Id))
            {
                var entity = new Publisher();
                _mapper.Map(model, entity);
                if (await _publisherService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDTO>>> Search(string txtSearch)
        {
            Expression<Func<Publisher, bool>> filter;
            // Create condition for Filter
            filter = a => a.Status != -1 && a.PubName!.Contains(txtSearch);
            var entityList = await _publisherService.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<PublisherDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

    }
}
