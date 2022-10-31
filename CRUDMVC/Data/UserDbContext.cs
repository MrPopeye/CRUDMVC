using Microsoft.EntityFrameworkCore;
using CRUDMVC.Models.UserModel;

namespace CRUDMVC.Data
{
    public class UserDbContext : DbContext
    {
        // Din cate am citit prin documentatii aici specific ce entitati sunt incluse in db
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
