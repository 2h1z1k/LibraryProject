using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject_Models.ViewModels
{
    public class BookVM
    {
        public Book? Book { get; set; }
        public IEnumerable<SelectListItem>? AuthorSelectList { get; set; }

        public IEnumerable<SelectListItem>? PublisherSelectList { get; set; }


    }
}
