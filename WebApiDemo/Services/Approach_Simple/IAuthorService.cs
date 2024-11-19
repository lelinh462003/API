using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using WebApiDemo.Data;
using WebApiDemo.DTO;
using WebApiDemo.Models;

namespace WebApiDemo.Services.Approach_Simple
{
    public interface IAuthorService
    {
        public Task<List<AuthorDTO>> getAllAsync();
        public Task<AuthorDTO> getAsync(int id);
        public Task<int> CreateAsync(AuthorDTO model);
        public Task UpdateAsync(int id, AuthorDTO model);
        public Task DeleteAsync(int id);
        Task<IEnumerable<AuthorDTO>> SearchAsync(Expression<Func<Author, bool>> expression, bool setDto = true);
    }

    public class AuthorService : IAuthorService
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public AuthorService(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> CreateAsync(AuthorDTO model)
        {
            var newAuthor = _mapper.Map<Author>(model);

            // Lấy ID lớn nhất hiện tại
            var maxId = await _context.Authors!.MaxAsync(a => (int?)a.Id) ?? 0;

            // Gán ID mới cho địa chỉ
            newAuthor.Id = maxId + 1;

            _context.Authors!.Add(newAuthor);
            await _context.SaveChangesAsync();

            return newAuthor.Id; // Trả về ID đã được gán
        }

        public async Task DeleteAsync(int id)
        {
            var deleteAuthor = _context.Authors!.SingleOrDefault(a => a.Id == id);
            if (deleteAuthor != null)
            {
                _context.Authors!.Remove(deleteAuthor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<AuthorDTO>> getAllAsync()
        {
            var authors = await _context.Authors!.ToListAsync();
            return _mapper.Map<List<AuthorDTO>>(authors);
        }

        public async Task<AuthorDTO> getAsync(int id)
        {
            var author = await _context.Authors!.FindAsync(id);
            return _mapper.Map<AuthorDTO>(author);
        }

        public async Task UpdateAsync(int id, AuthorDTO model)
        {
            if (id == model.Id)
            {
                var updateAuthor = _mapper.Map<Author>(model);
                _context.Authors!.Update(updateAuthor);
                await _context.SaveChangesAsync();
            }
        }
       

        public async Task<IEnumerable<AuthorDTO>> SearchAsync(Expression<Func<Author, bool>> expression, bool setDto = true)
        {
            if (setDto)
            {
                var authorList = await _context.Authors.Where(expression).ToListAsync();
                return _mapper.Map<IEnumerable<AuthorDTO>>(authorList);
            }
            else
            {
                var entityList = await _context.Authors
                    .Where(expression)
                    .Select(p => new Author
                    {
                        AuLname = p.AuLname,
                        AuFname = p.AuFname,
                        Phone = p.Phone,
                        Email = p.Email,
                        Address = p.Address,
                        Status = p.Status,
                    })
                    .ToListAsync();

                return _mapper.Map<IEnumerable<AuthorDTO>>(entityList);
            }
        }

    }
}
