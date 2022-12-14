using Microsoft.EntityFrameworkCore;
using UserManagement.Server.Common.Exceptions;
using UserManagement.Server.Interfaces;
using UserManagement.Server.Models.DbModels;
using UserManagement.Shared.Contracts.ManagementUser.Requests;

namespace UserManagement.Server.Services
{
    public class ManagementUserService : IManagementUserService
    {
        private readonly IUsersDbContext _context;

        public ManagementUserService(IUsersDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            return user;
        }
        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        public async Task RefreshStatusBlockAsync(UserBlockRequest userBlockRequest, CancellationToken cancellationToken)
        {
            var users = await _context.Users.Where(user => userBlockRequest.UsersId.Contains(user.Id)).ToListAsync(cancellationToken);

            users.ForEach(user => user.IsBlocked = userBlockRequest.StatusBlock);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await GetByIdAsync(userId, cancellationToken);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SetLastLoginDateAsync(DateTime loginTime, string id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            user.LastLoginDate = loginTime;

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
