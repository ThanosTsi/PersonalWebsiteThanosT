using PersonalWebsite.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebsite.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PersonalWebsite.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _db;
        internal DbSet<T> dbSet; 

        public Repository(ApplicationDBContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
                
            }
            else
            {
                query = dbSet.AsNoTracking();
            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includProperties))
            {
                foreach (var includeProp in includProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

        }

        //x,y,z
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, string? includProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter!= null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includProperties))
            {
                foreach (var includeProp in includProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
