using PersonalWebsite.DataAccess.Repository.IRepository;
using PersonalWebsite.DataAccess.Data;
using PersonalWebsite.DataAccess.Repository;
using PersonalWebsite.DataAccess.Repository.IRepository;
using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsite.DataAccess.Repository
{
    public class TestRepository : Repository<TestModel>, ITestRepository
    {
        private ApplicationDBContext _db;

        public TestRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void update(TestModel obj)
        {
            var objFromDb = _db.TestModels.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
            }
        }
    }
}

