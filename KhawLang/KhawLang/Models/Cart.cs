using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace KhawLang.Models
{
	public class Cart
	{
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string? CartID { get; set; }
		public int? CustomerID { get; set; }
		public decimal? TotalAmount { get; set; }
		public string? Status { get; set; }
		public Customer? Customer { get; set; }
		[NotMapped]
		public List<Meal>? MealItem { get; set; }
        public MealPlan? PlanItem { get; set; }
	}
}