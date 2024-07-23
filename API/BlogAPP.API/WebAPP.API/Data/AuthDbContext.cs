using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPP.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string adminUserId = "4e3c5cff-4462-460e-824b-86e40cb1a1fa";
            string readerRoleId = "60da2d34-ca9c-4e30-9102-8168b3a5a531";
            string writerRoleId = "82819708-7ce8-4349-89a6-c8c461ee506c";

            var roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpperInvariant(),
                    ConcurrencyStamp = readerRoleId
                    
                    /*
                        As the name state, it's used to prevent concurrency update conflict.

                        For example, there's a UserA named Peter in the database 2 admins open
                        the editor page of UserA, want to update this user.

                        Admin_1 opened the page, and saw user called Peter.
                        Admin_2 opened the page, and saw user called Peter (obviously).
                        Admin_1 updated user name to Tom, and save data. Now UserA in the db named Tom.
                        Admin_2 updated user name to Thomas, and try to save it.

                        What would happen if there's no ConcurrencyStamp is Admin_1's update will
                        be overwritten by Admin_2's update. But since we have ConcurrencyStamp, when
                        Admin_1/Admin_2 loads the page, the stamp is loaded. When updating data this
                        stamp will be changed too. So now step 5 would be system throw exception telling
                        Admin_2 that this user has already been updated, since he ConcurrencyStamp is
                        different from the one he loaded.
                    */
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpperInvariant(),
                    ConcurrencyStamp = writerRoleId
                },
                new IdentityRole
                {
                    Id = adminUserId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpperInvariant(),
                    ConcurrencyStamp = adminUserId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var admin = new IdentityUser() //TODO: remove the admin data from here
            {
                Id = "4e3c5cff-4462-460e-824b-86e40cb1a1fa",
                UserName = "admin@blogapp.com",
                NormalizedUserName = "admin@blogapp.com".ToUpperInvariant(),
                Email = "admin@blogapp.com",
                NormalizedEmail = "admin@blogapp.com".ToUpperInvariant(),
                EmailConfirmed = false,
                AccessFailedCount = 0,
                LockoutEnabled = true,
                ConcurrencyStamp = "4e3c5cff-4462-460e-824b-86e40cb1a1fa",
                LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(5),
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "`Standard`P@ssword`01234!");//TODO : remove the password!!!

            builder.Entity<IdentityUser>().HasData(admin);

            var adminRoles = new List<IdentityUserRole<string>>() {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = adminUserId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }

    }
}
