using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.IRepositories.IUserRepositories;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories.UserRepositories
{
    public class DistrictRepository : BaseRepository, IDistrictRepository
    {
        public DistrictRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(District district)
        {
            await _context.Districts.AddAsync(district);
        }

        public async Task<District> FindByIdAsync(int id)
        {
            return await _context.Districts.Include(d => d.Division).FirstOrDefaultAsync(d => d.Id == id);
        }

        public District GetDistrict(int id)
        {
            return _context.Districts.Include(d => d.Id == id).Where(d => d.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<District>> ListAsync()
        {
            return await _context.Districts.Include(d => d.Division).ToListAsync();
        }

        public void Remove(District district)
        {
            _context.Districts.Remove(district);
        }

        public void Update(District district)
        {
            _context.Districts.Update(district);
        }
    }
}
