using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fintelex_Assignment.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending";
        public bool IsDeleted { get; set; } = false;

        // Navigation properties (many-to-one)
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        // Navigation (1-to-many)
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
