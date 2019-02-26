using Microsoft.EntityFrameworkCore;

namespace MVCTest.Models.User
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
