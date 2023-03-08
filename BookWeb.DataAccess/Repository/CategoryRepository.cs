using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private  ApplicationDbConcext _db;
        public CategoryRepository(ApplicationDbConcext db) : base(db)
        {
            _db= db;
        }
       
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
