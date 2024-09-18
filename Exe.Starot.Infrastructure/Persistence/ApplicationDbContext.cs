using Exe.Starot.Domain.Common.Interfaces;
using Exe.Starot.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Exe.Starot.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            var migrator = this.Database.GetService<IMigrator>();
            migrator.Migrate();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            base.OnModelCreating(modelBuilder);
            // modelBuilder.ApplyConfiguration(new ChinhSachNhanSuConfiguration());

            ConfigureModel(modelBuilder);
        }
        private void ConfigureModel(ModelBuilder modelBuilder)
        {

        }
    }
}