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
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private  ApplicationDbConcext _db;
        public CoverTypeRepository(ApplicationDbConcext db) : base(db)
        {
            _db= db;
        }
       
        public void Update(CoverType obj)
        {
            _db.CoverTypes.Update(obj);
        }
    }
}
