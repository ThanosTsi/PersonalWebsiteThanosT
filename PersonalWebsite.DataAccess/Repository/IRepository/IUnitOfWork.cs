using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsite.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ITestRepository TestModel { get; }
        void Save();
    }
}
