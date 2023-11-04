using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace KhawLang.Models
{
	public class Tag
	{
		public int TagId { get; set; } // Primary Key
		public string? TagName { get; set; }
	}
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductId { get; set; } // Primary Key
		public string? Name { get; set; }
		public decimal? Price { get; set; }
		public int? Amount { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
		public virtual ICollection<Tag>? Tags { get; set; } // Many-to-Many relationship
	}

}

