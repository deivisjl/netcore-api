using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Configuration;
using System;

namespace PersistenceDbContext
{
    public class ApplicationDbContext : IdentityDbContext<User, Rol, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Identity");

            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder builder)
        {
            new ApplicationUserConfiguration(builder.Entity<User>());
            new ApplicationRolConfiguration(builder.Entity<Rol>());
        }
    }
}
