using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KhawLang.Data;
using KhawLang.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// DEBUG_FOR_CREATE_DATABASE

namespace KhawLang.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDBContext _db;
        public ProductController( ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> allProducts = _db.Products;
            return View(allProducts);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product Prodobj)
        {
            _db.Products.Add(Prodobj);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var cusdb = _db.Products.Find(Id);

            if (cusdb == null)
            {
                return NotFound();
            }
            return View(cusdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product Prodobj)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(Prodobj);
                _db.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }
            return View(Prodobj);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Products == null)
                return NotFound();
            var ProdDb = await _db.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (ProdDb == null)
                return NotFound();
            return View(ProdDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Products == null)
                return Problem("Entity set 'ApplicationDBContext.Products'  is null.");
            var prod = await _db.Products.FindAsync(id);
            if (prod != null)
                _db.Products.Remove(prod);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ProductExists (int id)
        {
            return (_db.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}

