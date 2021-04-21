using CoffeeAndSnackVendingMachine.Domain.Entities;

namespace CoffeeAndSnackVendingMachine.Persistence.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
