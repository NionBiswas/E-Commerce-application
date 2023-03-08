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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailsRepository
    {
        private  ApplicationDbConcext _db;
        public OrderDetailRepository(ApplicationDbConcext db) : base(db)
        {
            _db= db;
        }
       
        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
