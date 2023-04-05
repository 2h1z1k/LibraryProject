using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject_Models
{
    public class Book
    {

        //[Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public string? Keyword { get; set; }

        public string? Description { get; set; }
        public string? Image { get; set; }
        [Display(Name = "Author")]
        public int AuthorId { get; set; }
       // [ForeignKey("AuthorId")]
        public virtual Author? Author { get; set; }
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }
        //[ForeignKey("PublisherId")]
        public virtual Publisher? Publisher { get; set; }

    }
}
