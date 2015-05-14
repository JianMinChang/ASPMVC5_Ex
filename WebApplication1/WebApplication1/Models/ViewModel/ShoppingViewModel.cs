using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.ViewModel
{
    public class ShoppingViewModel
    {
        public int ListID { set; get; }

        public int UserID { set; get; }

        public bool Status { set; get; }

        public int Total { set; get; }

        public IList<ProductDetail> List { set; get; }
    }


    public class ProductDetail
    {
        public ProductDetail()
        {
            NoID = 0;
            ProductID = 0;
            Quantity = 0;
            Price = 0;
        }

        public int NoID { set; get; }

        public int ProductID { set; get; }

        [Display(Name = "產品名稱")]
        public string ProductName { set; get; }

        [Display(Name = "數量")]
        public int Quantity { set; get; }

        [Display(Name = "價錢")]
        public int Price { set; get; }
    }



    public class ProductParmsModel
    {
        public int ProductID { set; get; }
        public int Quantity { set; get; }
        public int Price { set; get; }
    }
}