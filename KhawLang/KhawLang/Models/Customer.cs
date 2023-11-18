using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace KhawLang.Models
{
	public class Customer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int? CustomerId { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }

		public string? Address { get; set; }
		public string? City { get; set; }
		public string? Postcode { get; set; }
		public string? Country { get; set; }
		public	PaymentInfo? PaymentItem { get; set; }
		public	Cart?	CartInfo { get; set; } 
	}

	public class PaymentInfo
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
		public string? CardNumber { get; set; }
		public DateTime? CardExpirationDate { get; set; }
		public string? CVV { get; set; }
	}

    public class Cart
	{
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int? CartID { get; set; }
		public int? CustomerID { get; set; }
		public int? TotalAmount { get; set; }
		public string? Status { get; set; }
		public Customer? Customer { get; set; }
		[NotMapped]
        public ICollection<MealTag>? MealTags { get; set; }
	}
}