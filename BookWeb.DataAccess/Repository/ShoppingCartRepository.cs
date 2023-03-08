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
    public class ShoppingCartRepository : Repository<ShoopingCart>, IShoppingCartRepository
    {
        private  ApplicationDbConcext _db;
        public ShoppingCartRepository(ApplicationDbConcext db) : base(db)
        {
            _db= db;
        }

        public int DecrementCount(ShoopingCart shoopingCart, int count)
        {
            shoopingCart.Count -= count;
            return shoopingCart.Count;
        }

        public int IncrementCount(ShoopingCart shoopingCart, int count)
        {
            shoopingCart.Count += count;
            return shoopingCart.Count;
        }
    }
}
