using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.IRepositories.IAccountRepositories;
using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories.AccountRepositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {

        }

        public async Task AddRole(ApplicationRole applicationRole)
        {
            await _context.ApplicationRoles.AddAsync(applicationRole);
        }

        public async Task<ApplicationRole> FindByIdAsync(string id)
        {
            return await _context.ApplicationRoles.FindAsync(id);
        }

        public async Task<IEnumerable<ApplicationRole>> ListOfRoles()
        {
            return await _context.ApplicationRoles.ToListAsync();
        }



        public async Task<IEnumerable<ApplicationUser>> ListOfUserInRole(string id)
        {
            return await _context.ApplicationUsers.Where(u => u.ApplicationRole.Id == id).ToListAsync();
        }

        public void RemoveRole(ApplicationRole applicationRole)
        {
            _context.ApplicationRoles.Remove(applicationRole);
        }

        public void UpdateRole(ApplicationRole applicationRole)
        {
            _context.ApplicationRoles.Update(applicationRole);
        }
    }
}
