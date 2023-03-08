using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.Models.ViewModel
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoopingCart> ListCart { get; set; }
        public OrderHeader OrderHeader { get; set; } 
    }
}
