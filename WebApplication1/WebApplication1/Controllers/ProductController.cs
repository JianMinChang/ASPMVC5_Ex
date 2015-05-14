using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Interface;
using WebApplication1.Models.Repository;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;

        public ProductController()
        {
            this._productRepository = new ProductRepository();
        }


        // GET: Product
        [AllowAnonymous]
        public ActionResult Index(string CategoryID = "")
        {
            var prod = this._productRepository.GetAll().ToList();

            if (!string.IsNullOrEmpty(CategoryID))
            {
                return View(prod.FindAll(x => x.CategoryID == int.Parse(CategoryID)));
            }
            else
            {
                return View(prod);
            }
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = this._productRepository.Get(x => x.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}