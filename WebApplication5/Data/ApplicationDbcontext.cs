using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Data
{
    

    public class ApplicationDbcontext : DbContext
    {

        public ApplicationDbcontext(DbContextOptions options): base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                                          
            modelBuilder.Entity<Product>().ToTable("product");

        
        }

    }
}
