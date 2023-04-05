using LibraryProject_Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject_DataAccess.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book obj);

        IEnumerable<SelectListItem> GetAllDropDownList(string obj);
    }
}
