using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiDemo.Data;

namespace WebApiDemo.Services.Second_Approach
{
    public interface IGenericService<T, TDto> where T : class
    {
        public Task<List<TDto>> GetAllAsync();
        public Task<TDto> GetByIdAsync(int id);
        public Task<int> PostAsync(TDto model);
        public Task PutAsync(int id, TDto model);
        public Task DeleteAsync(int id);
        Task<IEnumerable<TDto>> SearchAsync(Expression<Func<T, bool>> expression);
    }

    public class GenericService<T, TDto> : IGenericService<T, TDto> where T : class where TDto : class
    {
        private readonly ApiContext _dbContext;
        private readonly IMapper _mapper;
        private DbSet<T> _dbSet;
        public GenericService(ApiContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<int> PostAsync(TDto model)
        {
            var entity = _mapper.Map<T>(model);

            // Check if there are any elements in the set; if not, initialize the ID to 1
            var maxId = await _dbSet.AnyAsync() ? await _dbSet.MaxAsync(e => EF.Property<int>(e, "Id")) : 0;
            var idProperty = entity.GetType().GetProperty("Id");

            // Set the entity's ID
            idProperty.SetValue(entity, maxId + 1);

            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return (int)entity.GetType().GetProperty("Id").GetValue(entity);
        }

        public async Task PutAsync(int id, TDto model)
        {
            var entity = _mapper.Map<T>(model);
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TDto>> SearchAsync(Expression<Func<T, bool>> expression)
        {
            var entities = await _dbSet.Where(expression).ToListAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }
    }
}
