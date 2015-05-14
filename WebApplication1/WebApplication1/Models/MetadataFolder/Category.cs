using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
        private class CategoryMetadata
        {
            [DisplayName("分類編號")]
            public int CategoryID { get; set; }

            [DisplayName("分類名稱")]
            public string CategoryName { get; set; }

            [DisplayName("分類描述")]
            public string Description { get; set; }
        }
    }
}