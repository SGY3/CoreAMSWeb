using CoreAMSWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreAMSWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ProjectType> ProjectType { get; set; }
        public DbSet<ActivityType> ActivityType { get; set; }
        public DbSet<ActivityMaster> ActivityMaster { get; set; }
        public DbSet<ToDo> ToDo { get; set; }
    }
}
