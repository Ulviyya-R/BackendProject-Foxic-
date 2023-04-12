using Foxic_Backend_Project_.Entities;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Foxic_Backend_Project_.DAL
{
	public class FoxicDbContext:DbContext
	{
		public FoxicDbContext(DbContextOptions<FoxicDbContext> options) : base(options)
		{
			
		}
		public DbSet<Slider> Sliders { get; set; }
		public DbSet<Setting> Settings { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
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
