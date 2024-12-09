using Microsoft.EntityFrameworkCore;
using MVC_DEMO.Models;
namespace MVC_DEMO.Data
{
    public class ApplicationDbContext: DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Category_Id = 7,
                    CategoryName = "SkinCare",
                    DisplayOrder = 7
                });
        }
    }
}
