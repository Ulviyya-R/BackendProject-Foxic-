using Foxic_Backend_Project_.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Foxic_Backend_Project_.DAL
{
	public class FoxicDbContext:IdentityDbContext<User>
	{
		public FoxicDbContext(DbContextOptions<FoxicDbContext> options) : base(options)
		{
			
		}
		public DbSet<Slider> Sliders { get; set; }
		public DbSet<Setting> Settings { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Color> Colors { get; set; }
		public DbSet<Size> Sizes { get; set; }
		public DbSet<ProductSizeColor> ProductSizeColors { get; set; }

	
		public DbSet<GlobalTab> GlobalTabs { get; set; }
        public DbSet<Collection> Collections { get; set; }
		
		public DbSet<Product> Products { get; set; }






		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Setting>().
					HasIndex(s => s.Key).
					IsUnique();
			base.OnModelCreating(modelBuilder);
		}






	}
}
