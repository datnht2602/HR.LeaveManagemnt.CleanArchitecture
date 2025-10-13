using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>()
            {
                RoleId = "da897d77-679d-4873-8233-b0d1b85a93a7",
                UserId = "9e224968-33e4-4652-b7b7-8574d048cdb9"
            },
            new IdentityUserRole<string>()
            {
                RoleId = "f75c7adb-84fe-475d-a5f8-fac15d5fa12e",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );
    }
}