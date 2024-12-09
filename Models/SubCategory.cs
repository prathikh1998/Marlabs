using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Models.Models
{
    public class SubCategory
    {
        [Key]
        public int Category_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

    }
}
