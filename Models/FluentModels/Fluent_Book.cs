using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Models.Models
{
    public class Fluent_Book
    {
        //[Key]
        public int BookID { get; set; }
        public string Title { get; set; }
        //[MaxLength(20)]
        //[Required]
        public string ISBN { get; set; }
        public double Price { get; set; }

        //[NotMapped] //not there in the database
        public string PriceRange  { get; set; }

        ////navigation prop
        public Fluent_BookDetail Fluent_BookDetail { get; set; }

        //[ForeignKey("Publisher")]
        public int Publisher_Id { get; set; }
        public Fluent_Publisher Fluent_Publisher { get; set; }

        public List<Fluent_Author> Fluent_Authors { get; set; }
    }
}
