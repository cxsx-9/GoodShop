using System;
using KhawLang.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;

namespace KhawLang.Data
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options)
		{
			
		}
		// for fix one to one
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CartInfo)
                .WithOne(c => c.Customer)
                .HasForeignKey<Cart>(c => c.CustomerID);
            base.OnModelCreating(modelBuilder);
        }
		public DbSet<Meal> Meals { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<MealTag> MealTags { get; set; }
	}
}
