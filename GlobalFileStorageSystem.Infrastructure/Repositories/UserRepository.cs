using GlobalFileStorageSystem.Application.Contracts.Infrastructure;
using GlobalFileStorageSystem.Domain.Entities;
using GlobalFileStorageSystem.Infrastructure.Persistance;

namespace GlobalFileStorageSystem.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
