using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiDemo.Data;
using WebApiDemo.Models;
namespace WebApiDemo.Services.First_Approach
{
    public class PublisherService : IService<Publisher>
    {
        private readonly ApiContext _context;
        public PublisherService(ApiContext context)
        {
            _context = context;
        }

        public bool CheckExists(int id)
        {
            return _context.Publishers.Any(e => e.Id == id);
        }

        public async Task<Publisher> CreateAsync(Publisher entity)
        {
            _context.Publishers.Add(entity);
            if (await _context.SaveChangesAsync() > 0)
                return entity;

            return null!;
        }

        public int Delete(int id)
        {
            var entity = _context.Publishers.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<Publisher> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Publishers.FindAsync(id);
            else
            {
                var entity = await _context.Publishers
                               
                                .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<IEnumerable<Publisher>> GetListAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Publishers.AnyAsync()
            ? await _context.Publishers.MaxAsync(x => x.Id)
            : 0;
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Publishers.MinAsync(x => x.Id);
        }

        public async Task<Publisher> UpdateAsync(Publisher entity)
        {
            if (CheckExists(entity.Id))
            {
                _context.Entry(entity).State = EntityState.Modified;
                if (await _context.SaveChangesAsync() > 0)
                    return entity;
            }
            return null!;
        }

        public async Task<IEnumerable<Publisher>> SearchAsync(Expression<Func<Publisher, bool>> expression, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Publishers.Where(expression).ToListAsync();
            else
            {
                var list = await _context.Publishers
                        .Where(expression)
                        .Select(a => new Publisher
                        {
                            Id = a.Id,
                            PubName = a.PubName,
                            Phone = a.Phone,
                            Email = a.Email,
                            Address = a.Address,
                            Status = a.Status
                        })
                        .ToListAsync();
                return list;
            }
        }
    }
}
