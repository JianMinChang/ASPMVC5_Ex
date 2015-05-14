using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Interface;
using WebApplication1.Models.Repository;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Models.Service
{
    public class UserService
    {
        private IUserRepository _user;

        public UserService()
        {
            this._user = new UserRepository();
        }

        /// <summary>
        /// 檢查帳號是否重複
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public bool IsAccountRepet(string Account) 
        {
            var user = this._user.Get(x => x.Email == Account);
            if (  user!=null && user.UserID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 密碼加密
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public string PasswordEncoding(string Password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1");
        }

        /// <summary>
        /// 建立帳號
        /// </summary>
        /// <param name="user"></param>
        public void  Create (Users user)
        {
            this._user.Create(user);
        }

        /// <summary>
        /// 登出
        /// </summary>
        public void LogOut()
        {
            //清除Session中的資料
            HttpContext.Current.Session.Abandon();

            //登出表單驗證
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool Login(string Account, string Password)
        {
            // 登入時清空所有 Session 資料
            HttpContext.Current.Session.RemoveAll();

            var user = this._user.Get(x => x.Email == Account && x.Password == Password);

            if (user != null && user.UserID > 0)
            {
                // 將管理者登入的 Cookie 設定成 Session Cookie
                bool isPersistent = false;

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                  user.UserName,
                  DateTime.Now,
                  DateTime.Now.AddMinutes(30),
                  isPersistent,
                  "User",
                  FormsAuthentication.FormsCookiePath);

                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                
                FormsAuthentication.RedirectFromLoginPage(user.UserName, false);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}