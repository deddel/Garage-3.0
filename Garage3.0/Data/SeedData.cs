using Garage3._0.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Garage3._0.Data
{
    public class SeedData
    {
        private static ApplicationDbContext context = default!;
        private static RoleManager<IdentityRole> roleManager = default!;
        private static UserManager<ApplicationUser> userManager = default!;

        public static async Task InitAsync(ApplicationDbContext _context, IServiceProvider services)
        {
            context = _context;
            if (context.Roles.Any()) return;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var roleNames = new[] { "Member", "Admin" };
            //var adminEmail = "admin@admin.com";
            //var userEmail = "member@member.com";

            await AddRolesAsync(roleNames);

            var admin = await AddAccountAsync("Admin1337", "P@55w.rd", "Admin", "Adminsson", "123456-1234");
            var member = await AddAccountAsync("Member1337", "Pa55w.rd", "Member", "Membersson", "123456-4321");


            await AddUserToRoleAsync(admin, "Admin");
            await AddUserToRoleAsync(member, "Member");

        }

        private static async Task AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                var result = await userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded) throw new Exception(string.Join(", ", result.Errors.Select(x => "Code " + x.Code + " Description" + x.Description)));

            }
        }

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join(", ", result.Errors.Select(x => "Code " + x.Code + " Description" + x.Description)));
            }
        }

        private static async Task<ApplicationUser> AddAccountAsync(string userName, string pw, string fName, string lName, string socialSecurityNr)
        {
            var found = await userManager.FindByNameAsync(userName);

            if (found != null) return null!;

            var user = new ApplicationUser
            {
                UserName = userName,
                Password = pw,
                FName = fName,
                LName = lName,
                SocialSecurityNr = socialSecurityNr
            };

            var result = await userManager.CreateAsync(user, pw);

            if (!result.Succeeded) throw new Exception(string.Join(", ", result.Errors.Select(x => "Code " + x.Code + " Description" + x.Description)));

            return user;
        }
    }
}
