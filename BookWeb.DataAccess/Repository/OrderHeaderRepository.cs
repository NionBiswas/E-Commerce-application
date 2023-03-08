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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private  ApplicationDbConcext _db;
        public OrderHeaderRepository(ApplicationDbConcext db) : base(db)
        {
            _db= db;
        }
       
        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? PaymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if(orderFromDb != null)
            {
                orderFromDb.OrderStatus= orderStatus;   
                if(PaymentStatus != null)
                {
                    orderFromDb.PaymentStatus= PaymentStatus;
                }
            }
        }
    }
}
