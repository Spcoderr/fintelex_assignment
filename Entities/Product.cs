using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fintelex_Assignment.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.01")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation properties (many-to-one)
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        // Navigation (1-to-many)
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
