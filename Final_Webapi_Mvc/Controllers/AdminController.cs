using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_Webapi_Mvc.Models;
using PagedList;

namespace Final_Webapi_Mvc.Controllers
{
    public class AdminController : Controller
    {
        private J_JClothingEntities db = new J_JClothingEntities();


        // GET: Admin
        public ActionResult Index(string sort, int? page , int CatID=0)
        {
            //SortOrder 
            ViewBag.CurrentSort = sort;
            ViewBag.NameSortParm = sort == "Name" ? "name_desc" : "Name";
            ViewBag.PriceSortParm = sort == "Price" ? "price_desc" : "Price";

            //DropDownList
            ViewBag.Category = new SelectList(db.Categories, "CatID", "CatName", CatID);

            //ListItems
            var inventory = db.Inventories
                .Where(c => CatID ==0 || c.Category == CatID)
                .Select(c => new Clothing
                {
                    Image = c.ImageName,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price

                });
            switch (sort)
            {
                case "name_desc":
                    inventory = inventory.OrderByDescending(c => c.Name);
                    break;
                case "Name":
                    inventory = inventory.OrderBy(c => c.Name);
                    break;
                case "Price":
                    inventory = inventory.OrderBy(c => c.Price);
                    break;
                case "price_desc":
                    inventory = inventory.OrderByDescending(c => c.Price);
                    break;
                default:
                    inventory = inventory.OrderBy(c => c.Name);
                    break;
            }

            //Set paging 
            int pageSize = 3;
            int pagenumber = (page ?? 1);

            return View(inventory.ToPagedList(pagenumber, pageSize));
        }

        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.Categories, "CatID", "CatName");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SKU,Name,Description,Price,ImageName,Category")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Inventories.Add(inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(db.Categories, "CatID", "CatName", inventory.Category);
            return View(inventory);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = new SelectList(db.Categories, "CatID", "CatName", inventory.Category);
            return View(inventory);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SKU,Name,Description,Price,ImageName,Category")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category = new SelectList(db.Categories, "CatID", "CatName", inventory.Category);
            return View(inventory);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Inventory inventory = db.Inventories.Find(id);
            db.Inventories.Remove(inventory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
