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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private  ApplicationDbConcext _db;
        public ApplicationUserRepository(ApplicationDbConcext db) : base(db)
        {
            _db= db;
        }
       
      
    }
}
