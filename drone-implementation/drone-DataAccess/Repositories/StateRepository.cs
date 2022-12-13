using ddrone_DataAccess;
using drone_Domain.Entities;
using drone_Domain.Interfaces;

namespace drone_DataAccess.Repositories
{
    public class StateRepository: GenericRepository<State>,IStateRepository
    {
        public StateRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
