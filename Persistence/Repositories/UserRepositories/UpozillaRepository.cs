using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.IRepositories.IUserRepositories;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories.UserRepositories
{
    public class UpozillaRepository : BaseRepository, IUpozillaRepository
    {
        public UpozillaRepository(AppDbContext context) : base(context)
        {

        }

        public async Task AddAsync(Upozila upozilla)
        {
            await _context.Upozillas.AddAsync(upozilla);
        }

        public async Task<Upozila> FindByIdAsync(int id)
        {
            return await _context.Upozillas.Include(u => u.District).FirstOrDefaultAsync(u => u.Id == id);
        }

        public Upozila GetUpozilla(int id)
        {
            return _context.Upozillas.Where(u => u.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<Upozila>> ListAsync()
        {
            return await _context.Upozillas.Include(u => u.District).ToListAsync();
        }

        public void Remove(Upozila upozilla)
        {
            _context.Upozillas.Remove(upozilla);
        }

        public void Update(Upozila upozilla)
        {
            _context.Upozillas.Update(upozilla);
        }
    }
}
