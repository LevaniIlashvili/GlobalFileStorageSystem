using GlobalFileStorageSystem.Application.Contracts.Infrastructure.Repositories;
using GlobalFileStorageSystem.Domain.Entities;
using GlobalFileStorageSystem.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GlobalFileStorageSystem.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }
    }
}
