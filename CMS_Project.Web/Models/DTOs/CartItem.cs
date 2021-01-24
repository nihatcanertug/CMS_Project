﻿using CMS_Project.Entity.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Project.Web.Models.DTOs
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get => Quantity * Price; }
        public string Image { get; set; }

        public CartItem() { }

        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Price = product.UnitPrice;
            Quantity = 1;
            Image = product.Image;
        }
    }
}
