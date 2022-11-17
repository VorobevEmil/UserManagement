using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.Server.Interfaces;
using UserManagement.Server.Models.DbModels;

namespace UserManagement.Server.Data
{
    public class AppDbContext : IdentityDbContext<User>, IUsersDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
