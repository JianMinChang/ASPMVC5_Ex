using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ViewModel;
using System.Security.Principal;
using WebApplication1.Models.Service;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private UserService _userService;

        public AccountController()
        {
            this._userService = new UserService();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!this._userService.IsAccountRepet(model.Email))
            {
                if (ModelState.IsValid)
                {
                    Users user = new Users();
                    user.UserName = user.Email = model.Email;
                    user.Password = this._userService.PasswordEncoding(model.Password);

                    this._userService.Create(user);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "以存在該帳號";
            }
            
            // 如果執行到這裡，發生某項失敗，則重新顯示表單
            return View(model);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this._userService.Login(model.Email, this._userService.PasswordEncoding(model.Password));

            return View(model);
        }

        /// <summary>
        /// 後台使用者登出動作
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogOut()
        {
            this._userService.LogOut();

            //導至登入頁
            return RedirectToAction("Index", "Home");
        }

    }
}