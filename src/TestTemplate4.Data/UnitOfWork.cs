using System.Threading.Tasks;
using TestTemplate4.Common.Interfaces;

namespace TestTemplate4.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestTemplate4DbContext _dbContext;

        public UnitOfWork(TestTemplate4DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveAsync()
        {
            if (_dbContext.ChangeTracker.HasChanges())
            {
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }
    }
}