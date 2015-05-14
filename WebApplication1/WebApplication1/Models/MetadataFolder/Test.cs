using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [MetadataType(typeof(TestMetadata))]
    public partial class Tests
    {
        private class TestMetadata
        {
            [DisplayName("編號")]
            [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed)]
            public int ID { get; set; }

            [Required(ErrorMessage="請輸入中文姓名")]
            [StringLength(50,ErrorMessage="請勿輸入超過50個字")]
            [DisplayName("姓名")]
            public string Name { get; set; }

            [DisplayName("建立時間")]
            [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed)]
            public System.DateTime? InsertDateTime { get; set; }
        }
    }
}