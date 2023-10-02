using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key] // It is DataAnnotation
        public int Id { get; set; } // primary key becz of DataAnnotation

        [Required]
        [DisplayName("Category Name")]
        [MaxLength(20)]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "The field Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
