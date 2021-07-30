using Avocado.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Avocado.Database
{
    public class DAL : DbContext
    {
        public DAL(DbContextOptions<DAL> options) : base(options)
        {
        }

        public DAL()
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<MeterReading> MeterReadings { get; set; }

        public void AttachModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void Detach(object entity)
        {
            Entry(entity).State = EntityState.Detached;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).UseIdentityColumn();
            });

            modelBuilder.Entity<MeterReading>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).UseIdentityColumn();

                entity.HasOne(x => x.Account).WithMany(x => x.MeterReadings).HasForeignKey(x => x.AccountId);
            });
        }
    }
}