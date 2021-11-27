using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.IRepositories.IUserRepositories;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories.UserRepositories
{
    public class DivisionRepository : BaseRepository, IDivisionRepository
    {
        public DivisionRepository(AppDbContext context) : base(context)
        {

        }


        public async Task AddAsync(Division division)
        {
            await _context.Divisions.AddAsync(division);
        }

        public async Task<Division> FindByIdAsync(int id)
        {
            return await _context.Divisions.Include(d => d.Country).FirstOrDefaultAsync(d => d.Id == id);
        }

        public Division GetDivision(int id)
        {
            return _context.Divisions.Where(d => d.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<Division>> ListAsync()
        {
            return await _context.Divisions.Include(d => d.Country).ToListAsync();
        }

        public void Remove(Division division)
        {
            _context.Remove(division);
        }

        public void Update(Division division)
        {
            _context.Update(division);
        }
    }
}
