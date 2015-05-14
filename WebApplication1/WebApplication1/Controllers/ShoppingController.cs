using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Net;
using WebApplication1.Models;
using WebApplication1.Models.ViewModel;
using WebApplication1.Models.Status;
using Newtonsoft.Json;
using WebApplication1.Models.Service;


namespace WebApplication1.Controllers
{
    public class ShoppingController : Controller
    {
        private ShoppingService _shoppingService; 

        private IList<ProductDetail> list = new List<ProductDetail>();

        public ShoppingController()
        {
            this._shoppingService = new ShoppingService();
        }

        // GET: Shopping
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (this._shoppingService.HasSession())
            {
                list= this._shoppingService.GetSession();
            }
            else
            {
                list.Clear();
            }
            
            return View(list);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Create([Bind(Include = "ProductID,Quantity,Price")] ProductDetail  item)
        {
            AjaxStatus status = new AjaxStatus();

            item.ProductName = this._shoppingService.GetProductName(item.ProductID);
            
            list.Clear();
            if (this._shoppingService.HasSession())
            {
                list= this._shoppingService.GetSession();
                list.Add(item);
                status.IsSuccess = true;
            }
            else
            {
                list.Add(item);
                Session["ShoppingList"] = list;
                status.IsSuccess = true;
            }
            return Json(status);
        }


         [AllowAnonymous]
        public ActionResult Edit([Bind(Include="ProductID,Quantity,Price")] ProductParmsModel model )
        {
            ProductDetail Newlist = new ProductDetail();
            if (this._shoppingService.HasSession())
            {
                list= this._shoppingService.GetSession();

                Newlist = list.FirstOrDefault(x => x.ProductID == model.ProductID && x.Quantity == model.Quantity && x.Price == model.Price);

                if (Newlist == null )
                {
                    Response.Redirect(Url.Action("Index", "Shopping"), true);
                }
                else
                {
                    IList<SelectListItem> item = new List<SelectListItem>();
                    for (int i = 0; i <= this._shoppingService.GetProductQuantity(model.ProductID) ; i++)
                    {
                        item.Add(new SelectListItem() { Selected = model.Quantity == i ? true : false, Text = i.ToString(), Value = i.ToString() });
                    }
                    ViewBag.ProductDDL = item;
                }
            }
            else
            {
                Response.Redirect(Url.Action("Index","Shopping"),true);
            }

            return View(Newlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify([Bind(Include = "ProductID,Quantity,Price")] ProductParmsModel model)
        {
            ProductDetail Oldlist = new ProductDetail();
            if (this._shoppingService.HasSession())
            {
                list= this._shoppingService.GetSession();

                Oldlist = list.FirstOrDefault(x => x.ProductID == model.ProductID  && x.Price == model.Price);

                if (Oldlist == null)
                {
                    Response.Redirect(Url.Action("Index", "Shopping"), true);
                }
                else
                {
                    list.FirstOrDefault(x => x.ProductID == model.ProductID && x.Price == model.Price).Quantity = model.Quantity;
                    this._shoppingService.SetSession(list);
                }
                
            }
            
            return RedirectToAction("Index");
        }

         [AllowAnonymous]
        public ActionResult Delete([Bind(Include = "ProductID,Quantity,Price")] ProductParmsModel model)
        {

            ProductDetail Newlist = new ProductDetail();
            if (this._shoppingService.HasSession())
            {
                list= this._shoppingService.GetSession();

                Newlist = list.FirstOrDefault(x => x.ProductID == model.ProductID && x.Quantity == model.Quantity && x.Price == model.Price);

                if (Newlist==null)
                {
                    Response.Redirect(Url.Action("Index", "Shopping"), true);
                }
            }
            else
            {
                Response.Redirect(Url.Action("Index", "Shopping"), true);
            }
            return View(Newlist);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "ProductID,Quantity,Price")] ProductParmsModel model)
        {
            if (this._shoppingService.HasSession())
            {
                list= this._shoppingService.GetSession();

                var Newlist = list.FirstOrDefault(x => x.ProductID == model.ProductID && x.Quantity == model.Quantity && x.Price == model.Price);

                if (Newlist != null)
                {
                    list.Remove(Newlist);
                }
                this._shoppingService.SetSession(list);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkin()
        {
            if (this._shoppingService.HasSession())
            {
                if (this._shoppingService.AllHasinventory())
                {
                    this._shoppingService.ShoppingCheckin(User.Identity.Name, this._shoppingService.GetSession());

                    this._shoppingService.SetSession(null);
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public JsonResult GetShoppingHisory()
        {
            AjaxStatus ajaj = new AjaxStatus();
            IList<ShoppingViewModel> Data = this._shoppingService.GetUserShoppingHistory(User.Identity.Name);

            ajaj.IsSuccess = true;
            ajaj.JsonString = JsonConvert.SerializeObject(Data.OrderByDescending(x=>x.ListID));

            return Json(ajaj);
        }

    }
}