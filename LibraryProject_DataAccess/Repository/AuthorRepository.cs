using LibraryProject_DataAccess.Data;
using LibraryProject_DataAccess.Repository.IRepository;
using LibraryProject_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject_DataAccess.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _db;

        public AuthorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Author obj)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
            }
        }
    }
}
