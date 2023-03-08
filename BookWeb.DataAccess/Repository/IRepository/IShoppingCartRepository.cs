﻿using BookWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoopingCart>
    {
        int IncrementCount(ShoopingCart shoopingCart, int count);
        int DecrementCount(ShoopingCart shoopingCart, int count);
      
    }
}
