using PersonalWebsite.DataAccess.Data;
using PersonalWebsite.DataAccess.Repository.IRepository;
using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsite.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _db;
        public ITestRepository TestModel { get; private set; }
        public UnitOfWork(ApplicationDBContext db)
        {
            _db = db;
            TestModel = new TestRepository(_db);
        }
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
