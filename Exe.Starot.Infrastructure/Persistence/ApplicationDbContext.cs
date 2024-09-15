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
            var user = new UserEntity
            {
                Email = "manager@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Manager"
            };

            var user1 = new UserEntity
            {
                Email = "business@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Business"
            };

        

            modelBuilder.Entity<UserEntity>().HasData(user);
            modelBuilder.Entity<UserEntity>().HasData(user1);
           
        }
    }
}