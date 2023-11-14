using System;
using KhawLang.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;

namespace KhawLang.Data
{
	public static class PostgreSqlDemoDbContextConfigurer
	{

	}
	public class ApplicationDBContext : DbContext
	{
		public void Configure(DbContextOptionsBuilder<ApplicationDBContext> builder, string connectionString)
		{
			builder.UseNpgsql(connectionString);
		}

		public void Configure(DbContextOptionsBuilder<ApplicationDBContext> builder, DbConnection connection)
		{
			builder.UseNpgsql(connection);
		}
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options)
		{
			
		}
		public DbSet<Meal> Meals { get; set; } // ตัวแทนตารางที่เก็บข้อมูล การเข้าถึงข้อมูลต้องผ่านตัวนี้

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>()
                .Property(p => p.MealId)
                .ValueGeneratedOnAdd();
        }
	}
}