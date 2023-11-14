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
    public class MealController : Controller
    {
        private readonly ApplicationDBContext _db;
        public MealController( ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Meal> allMeals = _db.Meals;
            return View(allMeals);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Meal Item)
        {
            // Item.Tags = Item.TagsInput?.Split(',').Select(tag => tag.Trim()).ToList();
            if (!string.IsNullOrEmpty(Item.TagsInput))
            {
                List<string> tagsArray = Item.TagsInput.Split(',').Select(tag => tag.Trim()).ToList();
                Item.Tags = tagsArray;
                Console.WriteLine("Tags: " + string.Join(", ", Item.Tags)); // debug
            }
            else
            {
                Console.WriteLine("Cerate a new one without data."); // debug
                Item.Tags = new List<string>();
            }
            _db.Meals.Add(Item);
            _db.SaveChanges();
            Console.WriteLine("Meal created successfully."); // debug
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var cusdb = _db.Meals.Find(Id);

            if (cusdb == null)
            {
                return NotFound();
            }
            return View(cusdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Meal Item)
        {
            if (ModelState.IsValid)
            {
                _db.Meals.Update(Item);
                _db.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }
            return View(Item);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Meals == null)
                return NotFound();
            var mealDb = await _db.Meals.FirstOrDefaultAsync(m => m.MealId == id);
            if (mealDb == null)
                return NotFound();
            return View(mealDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Meals == null)
                return Problem("Entity set 'ApplicationDBContext.Meals'  is null.");
            var meal = await _db.Meals.FindAsync(id);
            if (meal != null)
                _db.Meals.Remove(meal);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool MealExists (int id)
        {
            return (_db.Meals?.Any(e => e.MealId == id)).GetValueOrDefault();
        }
    }
}

