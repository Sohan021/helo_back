using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.IRepositories.IUserRepositories;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories.UserRepositories
{
    public class UnionOrWardRepository : IUnionOrWardRepository
    {
        private readonly AppDbContext _context;

        public UnionOrWardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UnionOrWard union)
        {
            await _context.UnionOrWards.AddAsync(union);
        }

        public async Task<UnionOrWard> FindByIdAsync(int id)
        {
            return await _context.UnionOrWards.Include(up => up.Upozila).ThenInclude(u => u.District).ThenInclude(j => j.Division).Where(u => u.Id == id).FirstOrDefaultAsync(u => u.Id == id);
        }

        public UnionOrWard GetUnion(int id)
        {
            return _context.UnionOrWards.Where(u => u.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<UnionOrWard>> ListAsync()
        {
            return await _context.UnionOrWards.Include(up => up.Upozila).ThenInclude(u => u.District).ThenInclude(j => j.Division).ToListAsync();
        }

        public void Remove(UnionOrWard union)
        {
            _context.UnionOrWards.Remove(union);
        }

        public void Update(UnionOrWard union)
        {
            _context.UnionOrWards.Update(union);
        }
    }
}
