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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private  ApplicationDbConcext _db;
        public CompanyRepository(ApplicationDbConcext db) : base(db)
        {
            _db= db;
        }
       
        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
