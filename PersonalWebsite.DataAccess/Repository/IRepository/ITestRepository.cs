using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsite.DataAccess.Repository.IRepository
{
    public interface ITestRepository : IRepository<TestModel>
    {
        void update(TestModel testModel);
    }
}
