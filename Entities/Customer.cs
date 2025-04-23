using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fintelex_Assignment.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation property (1-to-many)
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
