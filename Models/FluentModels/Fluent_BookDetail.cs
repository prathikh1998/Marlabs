using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Models.Models
{
    public class Fluent_BookDetail
    {
       // [Key]
        public int BookDetail_Id { get; set; }
        //[Required]
        public int NumberOfChapters { get; set; }

        public int NumberOfPages { get; set; }

        public double Weight { get; set; }

        ////[ForeignKey("Book")]
        public int BookID { get; set; }

        public Fluent_Book Fluent_Book { get; set; }


    }
}
