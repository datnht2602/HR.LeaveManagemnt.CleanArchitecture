using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole()
            {
                Id = "da897d77-679d-4873-8233-b0d1b85a93a7",
                Name = "Employee",
                NormalizedName = "EMPLOYEE",
            },
            new IdentityRole()
            {
                Id = "f75c7adb-84fe-475d-a5f8-fac15d5fa12e",
                Name = "Admin",
                NormalizedName = "ADMIN",           
            }
        );
    }
}