using System.ComponentModel.DataAnnotations;

namespace CryptoOcean.Models
{
	public class Item
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		[Range(1, 100, ErrorMessage = "Price must be between 1 and 100 only!")]
		public int Price { get; set; }
		public DateTime CreatedDateTime { get; set; } = DateTime.Now;
	}
}
