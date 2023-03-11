
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
