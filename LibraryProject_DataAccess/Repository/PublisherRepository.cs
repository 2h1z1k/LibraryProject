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
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        private readonly ApplicationDbContext _db;

        public PublisherRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Publisher obj)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
            }
        }
    }
}
