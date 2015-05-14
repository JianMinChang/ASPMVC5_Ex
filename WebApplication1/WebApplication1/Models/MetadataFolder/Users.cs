using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication1.Models.MetadataFolder
{
    [MetadataType(typeof(UsersMetadata))]
    public partial class Users
    {
        private class UsersMetadata
        {
            [DisplayName("使用者序號")]
            public int UserID { get; set; }

            [DisplayName("帳號")]
            public string Email { get; set; }

            [DisplayName("密碼")]
            public string Password { get; set; }

            [DisplayName("使用者名稱")]
            public string UserName { get; set; }
        }
    }
}