using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VagtplanDbContext _db;

        public UserRepository(VagtplanDbContext db) => _db = db;

        public async Task<User> AddUserAsync(User user, CancellationToken ct = default)
        {
            await _db.Users.AddAsync(user, ct);
            await _db.SaveChangesAsync(ct);
            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId, ct);
            if (user == null) return false;

            _db.Users.Remove(user);
            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> ExistsByEmailAsync(string email, Guid? userId = null, CancellationToken ct = default)
        {
            var normalized = email.Trim().ToUpperInvariant();
            var query = _db.Users.AsNoTracking().Where(u => u.NormalizedEmail == normalized);

            // Exclude a specific user ID if provided (for updating existing user)
            if (userId.HasValue)
                query = query.Where(u => u.Id != userId.Value);

            return await query.AnyAsync(ct);
        }

        public async Task<User?> GetAdminInfoAsync(CancellationToken ct = default)
        {
            return await _db.Users
                .Include(u => u.Permission)
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    u.Permission != null &&
                    u.Permission.Read &&
                    u.Permission.Write &&
                    u.Permission.Edit &&
                    u.Permission.Delete,
                    ct);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken ct = default)
        {
            return await _db.Users
                .Include(u => u.Department)
                .Include(u => u.DoctorType)
                .Include(u => u.Permission)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId, ct);
        }

        public async Task<IReadOnlyList<User>> GetUsersAsync(bool onlyActive, CancellationToken ct = default)
        {
            var query = _db.Users
                .Include(u => u.Department)
                .Include(u => u.DoctorType)
                .Include(u => u.Permission)
                .AsNoTracking();

            // Filter by active status if specified
            if (onlyActive)
            {
                query = query.Where(u => u.UserStatus == UserStatus.Active);
            }

            return await query.OrderBy(u => u.FullName).ToListAsync(ct);
        }

        public async Task<User> UpdateUserAsync(User user, CancellationToken ct = default)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync(ct);
            return user;
        }
    }
}
