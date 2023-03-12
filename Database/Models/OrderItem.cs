using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int OrderId { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Quantity { get; set; }
        [Required]
        public string Unit { get; set; }
    }
}
