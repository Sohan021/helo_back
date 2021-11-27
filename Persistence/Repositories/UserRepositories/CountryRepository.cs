using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.IRepositories.IUserRepositories;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories.UserRepositories
{
    public class CountryRepository : BaseRepository, ICountryRepository
    {


        public CountryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Country country)
        {
            await _context.Countries.AddAsync(country);
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(a => a.Id == id).FirstOrDefault();
        }

        public async Task<Country> FindByIdAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<IEnumerable<Country>> ListAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public void Remove(Country country)
        {
            _context.Countries.Remove(country);
        }

        public void Update(Country country)
        {
            _context.Countries.Update(country);
        }
    }
}
