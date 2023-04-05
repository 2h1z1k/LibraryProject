using LibraryProject_DataAccess.Data;
using LibraryProject_DataAccess.Repository.IRepository;
using LibraryProject_Models;
using LibraryProject_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject_DataAccess.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem>? GetAllDropDownList(string obj)
        {
            if (obj == WC.AuthorName)
            {
                return _db.Author.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }
            if (obj == WC.PublisherName)
            {
                return _db.Publisher.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }
            return null;
        }

        public void Update(Book obj)
        {
            _db.Book.Update(obj);
        }
    }
}
