using TestTemplate4.Core.Entities;
using TestTemplate4.Core.Interfaces;

namespace TestTemplate4.Data.Repositories
{
    public class FooRepository : Repository<Foo>, IFooRepository
    {
        public FooRepository(TestTemplate4DbContext context)
            : base(context)
        {
        }
    }
}
