using EnergyAccountManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnergyAccountManagement.DataAccess
{
    public class AccountManagementDbContext : DbContext
    {
        public AccountManagementDbContext(DbContextOptions<AccountManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(x => x.AccountId).IsUnique();
            });
        }
    }
}
