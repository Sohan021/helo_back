using ofarz_rest_api.Domain.IRepositories;
using ofarz_rest_api.Persistence.Context;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
