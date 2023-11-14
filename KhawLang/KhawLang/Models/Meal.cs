using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace KhawLang.Models
{
	public class Meal
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MealId { get; set; }
		public string? Name { get; set; }
		public decimal? Price { get; set; }
		public int? Amount { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
		[NotMapped]
		public List<string>? Tags { get; set; }
		public string? TagsInput { get; set; }
	}
}

