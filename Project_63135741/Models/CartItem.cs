using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_63135741.Models
{
    public class CartItem
    {
        public string FoodID { get; set; }
        public string FoodsName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}