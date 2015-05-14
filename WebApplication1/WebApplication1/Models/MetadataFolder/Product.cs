using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        private class ProductMetadata
        {
           
            [DisplayName("產品編號")]
            public int ProductID { get; set; }

            [DisplayName("分類編號")]
            public int CategoryID { get; set; }

            [DisplayName("產品名稱")]
            public string ProductName { get; set; }

            [DisplayName("庫存數量")]
            public int Quantity { get; set; }

            [DisplayName("產品描述")]
            public string Description { get; set; }

            [DisplayName("單價")]
            public int Price { get; set; }
        }
    }
}