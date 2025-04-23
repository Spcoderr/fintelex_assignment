using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fintelex_Assignment.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation property (1-to-many)
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
