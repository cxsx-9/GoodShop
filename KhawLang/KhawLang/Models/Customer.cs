using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace KhawLang.Models
{
	public class Customer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string? CustomerId { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public	AddressInfo? AddressItem { get; set; }
		public	PaymentInfo? PaymentItem { get; set; }
		public	Cart?	CartInfo { get; set; } 
		public	MealPlan?	MealPlanInfo { get; set; }
	}
	public class AddressInfo
	{
		public string? Address { get; set; }
		public string? City { get; set; }
		public string? Postcode { get; set; }
		public string? Country { get; set; }
	}

	public class PaymentInfo
	{
		public string? CardNumber { get; set; }
		public DateTime? CardExpirationDate { get; set; }
		public string? CVV { get; set; }
		public List<MealPlan>? MealPlans { get; set; }
	}
}