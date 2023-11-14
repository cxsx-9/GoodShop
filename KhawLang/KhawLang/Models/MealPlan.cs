using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace KhawLang.Models
{
	public class MealPlan
	{
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string? PlanID { get; set; }
		public int? CustomerID { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public decimal? TotalAmount { get; set; }
		public string? DeliveryAddress { get; set; }
		public string? Status { get; set; }
		public Customer? Customer { get; set; }
		[NotMapped]
		public List<Meal>? MealItem { get; set; }
	}
}