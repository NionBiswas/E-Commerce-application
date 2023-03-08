using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbConcext _db;
        internal DbSet<T> Dbset;
        public Repository(ApplicationDbConcext db)
        {
            _db= db;
           this.Dbset= _db.Set<T>();
        }
        public void Add(T entity)
        {
           Dbset.Add(entity);
        }
        //include properties - "Category,Covertype"
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? Filter=null, string? includeProperties = null)
        {
            IQueryable<T> qurry = Dbset;
            if(Filter != null)
            {
                qurry = qurry.Where(Filter);
            }
            if (includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    qurry = qurry.Include(includeProp);
                } 
            }
            return qurry.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> Filter, string? includeProperties = null)
        {
            IQueryable<T> qurry = Dbset;
            qurry = qurry.Where(Filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    qurry = qurry.Include(includeProp);
                }
            }
            return qurry.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            Dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            Dbset.RemoveRange(entity);
        }
    }
}

