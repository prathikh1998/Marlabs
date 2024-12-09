using System.ComponentModel.DataAnnotations;

namespace MVC_DEMO.Models
{
    public class Category
    {
        [Key]
        public int Category_Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string CategoryName { get; set; }
        [Range(1,100, ErrorMessage ="Display order must be bw 1 to 100")]
        public int DisplayOrder { get; set; }

    }
}
