using MyBackend.Models;

namespace MyBackend.Data;

public static class DbSeeder
{
    public static void SeedData(AppDbContext context)
    {
        // Check if database already has data
        if (context.Users.Any()) return;

        // Create roles
        var adminRole = new Role { Rolename = "ADMIN" };
        var userRole = new Role { Rolename = "USER" };
        context.Roles.Add(adminRole);
        context.Roles.Add(userRole);
        context.SaveChanges();

        // Create admin user with both roles
        var adminUser = new User
        {
            Username = "admin",
            Password = BCrypt.Net.BCrypt.HashPassword("admin"),
            Roles = new List<Role> { adminRole, userRole }
        };
        context.Users.Add(adminUser);

        // Create regular user with user role
        var regularUser = new User
        {
            Username = "user",
            Password = BCrypt.Net.BCrypt.HashPassword("user"),
            Roles = new List<Role> { userRole }
        };
        context.Users.Add(regularUser);

        context.SaveChanges();
    }
}
