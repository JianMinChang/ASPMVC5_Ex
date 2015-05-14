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
    public class CategoriesController : Controller
    {
        private ICategoriesRepository categoryRepository;

        public CategoriesController()
        {
            this.categoryRepository = new CategoriesRepository();
        }

        // GET: Categories
        public ActionResult Index()
        {
            return View(this.categoryRepository.GetAll());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Category category = this.categoryRepository.Get(x => x.CategoryID == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                this.categoryRepository.Create(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = this.categoryRepository.Get(x => x.CategoryID == id);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,Description")] Category category)
        {
            if (category !=null &&  ModelState.IsValid)
            {
                this.categoryRepository.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = this.categoryRepository.Get(x => x.CategoryID == id);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Category category = this.categoryRepository.Get(x => x.CategoryID == id);
                this.categoryRepository.Delete(category);
            }
            catch {
                return RedirectToAction("Delete", new { id = id });
            }
            return RedirectToAction("Index");
        }

    }
}
