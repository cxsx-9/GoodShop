using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhawLang.Models
{
	public class Meal
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MealId { get; set; }
		public string? Name { get; set; }
		public int? Price { get; set; }
		public int? Amount { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
		public List<Tag>? Tags { get; set; }
	}

	public class Tag
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TagId { get; set; }
		public string? Name { get; set; }
		public ICollection<Meal>? Meals { get; set; }
	}

	public class MealTag
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MealTagId { get; set; }

		public int MealId { get; set; }
		public Meal? Meal { get; set; }
	
		public int TagId { get; set; }
		public Tag? Tag { get; set; }

		public ICollection<MealPlan>? MealPlans { get; set; }
	}
	public class EditMealTagViewModel
	{
		[Required(ErrorMessage = "Please select a meal")]
		[Display(Name = "Select Meal")]
		public IEnumerable<SelectListItem> SelectedMealId { get; set; }

		[Display(Name = "Select Tags")]
		public List<int> SelectedTagIds { get; set; }
		public SelectList Meals { get; set; }
		public List<Tag> Tags { get; set; }

	}

	public class MealPlan
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PlanID { get; set; }
		public int? CustomerID { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public int? TotalAmount { get; set; }
		public string? DeliveryAddress { get; set; }
		public string? Status { get; set; }
		public Customer? Customer { get; set; }
		public ICollection<MealTag>? MealTags { get; set; }
	}
}

