using System.Threading.Tasks;
using webapi_m_sqlserver.Domain.Repositories;

namespace webapi_m_sqlserver.Persistence.Repositories
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