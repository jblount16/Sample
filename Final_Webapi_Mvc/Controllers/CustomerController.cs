using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Final_Webapi_Mvc.Models;

namespace Final_Webapi_Mvc.Controllers
{
    public class CustomerController : ApiController
    {
        private J_JClothingEntities db = new J_JClothingEntities();

        // GET: api/Customer
        [Route("category")]
        public IQueryable<Clothing> GetCategories()
        {
            var category = db.Categories
                .OrderBy(c => c.CatName)
                .Select(c => new Clothing
                {
                    CatID = c.CatID,
                    CatName = c.CatName
                });
            return category;
        }

        //GET: /product returns list of product
        [Route("products")]
        public IQueryable<Clothing> GetProducts()
        {
            var inventory = db.Inventories
                .OrderBy(c => c.Name)
               
                .Select(c => new Clothing
                {
                    SKU = c.SKU,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price
                });

            return inventory;
        }

        // GET: api/Customer/5
        [Route("products/{id:int}")]
        [ResponseType(typeof(Clothing))]
        public IHttpActionResult GetProduc(int id=0)
        {
            var inventory = db.Inventories
                .OrderBy(c => c.Name)
                .Where(c => c.Category == id)
                .Select(c => new Clothing
                {
                    SKU = c.SKU,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price,
                }).ToList();
            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory);
        }

        // PUT: api/Customer/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.CatID)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customer
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CategoryExists(category.CatID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = category.CatID }, category);
        }

        // DELETE: api/Customer/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.CatID == id) > 0;
        }
    }
}