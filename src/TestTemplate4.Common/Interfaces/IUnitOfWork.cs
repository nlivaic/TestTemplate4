using System.Threading.Tasks;

namespace TestTemplate4.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}