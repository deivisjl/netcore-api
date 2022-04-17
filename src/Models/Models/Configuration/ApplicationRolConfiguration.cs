using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Configuration
{
    public class ApplicationRolConfiguration
    {
        public ApplicationRolConfiguration(EntityTypeBuilder<Rol> entity)
        {
            entity.HasKey(x => x.Id);
            entity.HasData(
                new Rol
                {
                    Id = Guid.NewGuid().ToString().ToLower(),
                    Name = "Admin"
                }
            );

            entity.HasMany(e => e.UserRol).WithOne(e => e.Rol).HasForeignKey(e => e.RoleId).IsRequired();
        }
    }
}
