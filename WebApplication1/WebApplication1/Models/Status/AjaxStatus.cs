using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Status
{
    public class AjaxStatus
    {
        public AjaxStatus()
        {
            this.IsSuccess = false;
            this.JsonString = string.Empty;
            this.Message = string.Empty;
        }

        public bool IsSuccess { set; get; }
        public string Message { set; get; }
        public string JsonString { set; get; }

    }
}