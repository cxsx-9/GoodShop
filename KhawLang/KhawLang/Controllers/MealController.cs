using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KhawLang.Data;
using KhawLang.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KhawLang.Controllers
{
	public class MealController : Controller
	{
		private readonly ApplicationDBContext _db;
		public MealController( ApplicationDBContext db)
		{
			_db = db;
		}

// -------- TAG controller TAG controller TAG controller TAG controller TAG controller TAG controller TAG controller

		public IActionResult IndexTag()
		{
			IEnumerable<Tag> allTags = _db.Tags;
			return View(allTags);
		}
		public IActionResult CreateTag()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateTag(Tag Item)
		{
			_db.Tags.Add(Item);
			_db.SaveChanges();
			Console.WriteLine("Tag created successfully."); // debug
			return RedirectToAction(nameof(IndexTag));
		}
		public async Task<IActionResult> DeleteTag(int? id)
		{
			if (id == null || _db.Tags == null)
				return NotFound();
			var tagDb = await _db.Tags.FirstOrDefaultAsync(m => m.TagId == id);
			if (tagDb == null)
				return NotFound();
			return View(tagDb);
		}

		public IActionResult EditTag(int? Id)
		{
		    if (Id == null || Id == 0)
		    {
		        return NotFound();
		    }
		    var tag = _db.Tags.Find(Id);

		    if (tag == null)
		    {
		        return NotFound();
		    }
		    return View(tag);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditTag(Tag Item)
		{
		    if (ModelState.IsValid)
		    {
		        _db.Tags.Update(Item);
		        _db.SaveChanges();
		        TempData["ResultOk"] = "Data Updated Successfully !";
		        return RedirectToAction("IndexTag");
		    }
		    return View(Item);
		}

		[HttpPost, ActionName("DeleteTag")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteTagConfirmed(int id)
		{
			if (_db.Tags == null)
				return Problem("Entity set 'ApplicationDBContext.Tags'  is null.");
			var Tag = await _db.Tags.FindAsync(id);
			if (Tag != null)
				_db.Tags.Remove(Tag);
			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(IndexTag));
		}
		private bool TagExists (int id)
		{
			return (_db.Tags?.Any(e => e.TagId == id)).GetValueOrDefault();
		}
// option -- Meal Tag
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult EditMealTag(EditMealTagViewModel viewModel)
{
    // Validate the model state
    if (!ModelState.IsValid)
    {
        // If validation fails, return the view with the current model
        return View(viewModel);
    }

    // Retrieve the selected meal from the database
    var selectedMeal = _db.Meals.Find(viewModel.SelectedMealId);

    // Check if the meal exists
    if (selectedMeal == null)
    {
        return NotFound();
    }

    // Validate selected tags
    var validTagIds = _db.Tags.Where(tag => viewModel.SelectedTagIds.Contains(tag.TagId)).Select(tag => tag.TagId).ToList();

    // Create MealTags for valid selected tags
    if (validTagIds.Any())
    {
        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                foreach (var tagId in validTagIds)
                {
                    // Create a new MealTag
                    var mealTag = new MealTag
                    {
                        MealId = selectedMeal.MealId,
                        TagId = tagId
                    };
                    // Add the new MealTag to the database
                    _db.MealTags.Add(mealTag);
                }
                _db.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                // Handle the exception (e.g., log it or return an error view)
                ModelState.AddModelError(string.Empty, "An error occurred while saving changes.");
                return View(viewModel);
            }
        }
    }

    // Redirect to the Index action or another appropriate action
    return RedirectToAction(nameof(Index));
}

// option



// -------- MEAL controller MEAL controller MEAL controller MEAL controller MEAL controller MEAL controller

		public IActionResult IndexMeal()
		{
			IEnumerable<Meal> allMeals = _db.Meals;
			return View(allMeals);
		}
		public IActionResult Index()
		{
			IEnumerable<MealTag> mealTags = _db.MealTags.Include(mt => mt.Meal).Include(mt => mt.Tag);
			var groupedMeals = mealTags.GroupBy(mt => mt.Meal);
			return View(groupedMeals);
		}
		public IActionResult Create()
		{
			ViewBag.Tags = _db.Tags.ToList();
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Meal meal, int[] selectedTags)
		{
			// Add meal details to the database
			_db.Meals.Add(meal);
			_db.SaveChanges();
			// Associate selected tags with the created meal
			if (selectedTags != null && selectedTags.Length > 0)
			{
				foreach (var tagId in selectedTags)
				{
					var tag = _db.Tags.Find(tagId);
					if (tag != null)
					{
						_db.MealTags.Add(new MealTag { MealId = meal.MealId, TagId = tagId });
					}
				}
				_db.SaveChanges();
			}
			Console.WriteLine($"Meal created with ID: {meal.MealId}"); // debug
			// Check associated tags
			var associatedTags = _db.MealTags.Where(mt => mt.MealId == meal.MealId).Select(mt => mt.Tag.Name).ToList();
			Console.WriteLine("Associated Tags:");
			foreach (var tag in associatedTags)
			{
				Console.WriteLine(tag);
			}
			// Redirect to the Index action
			return RedirectToAction(nameof(Index));
		}


// -------- Edit new version
		public IActionResult Edit(int id)
		{
			Meal meal = _db.Meals.Include(m => m.Tags).FirstOrDefault(m => m.MealId == id);

			if (meal == null)
			{
				return NotFound();
			}

			// Retrieve all tags to populate the checkbox list
			ViewBag.Tags = _db.Tags.ToList();

			return View(meal);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Meal meal, int[] selectedTags)
		{
			if (ModelState.IsValid)
			{
				// Update meal details in the database
				_db.Meals.Update(meal);
				_db.SaveChanges();

				// Update associated tags
				UpdateMealTags(meal.MealId, selectedTags);

				Console.WriteLine($"Meal updated with ID: {meal.MealId}"); // debug

				return RedirectToAction(nameof(Index));
			}

			// If ModelState is not valid, retrieve all tags to populate the checkbox list
			ViewBag.Tags = _db.Tags.ToList();
			
			return View(meal);
		}

		private void UpdateMealTags(int mealId, int[] selectedTags)
		{
			// Remove existing tags associated with the meal
			var existingTags = _db.MealTags.Where(mt => mt.MealId == mealId);
			_db.MealTags.RemoveRange(existingTags);

			// Add new tags to the meal
			if (selectedTags != null && selectedTags.Length > 0)
			{
				foreach (var tagId in selectedTags)
				{
					var tag = _db.Tags.Find(tagId);
					if (tag != null)
					{
						_db.MealTags.Add(new MealTag { MealId = mealId, TagId = tagId });
					}
				}
			}

			_db.SaveChanges();
		}
// ----- delete
		public IActionResult Delete(int id)
		{
			var meal = _db.Meals.Include(m => m.Tags).FirstOrDefault(m => m.MealId == id);

			if (meal == null)
			{
				return NotFound();
			}
			return View(meal);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var meal = _db.Meals.Find(id);

			if (meal == null)
			{
				return NotFound();
			}
			_db.Meals.Remove(meal);
			_db.SaveChanges();
			return RedirectToAction(nameof(Index));
		}


// -------- old version of Create
		// public IActionResult Create()
		// {
		//     return View();
		// }

		// [HttpPost]
		// [ValidateAntiForgeryToken]
		// public IActionResult Create(MealTag Item)
		// {
		//     _db.MealTags.Add(Item);
		//     _db.SaveChanges();
		//     Console.WriteLine("Meal created successfully."); // debug
		//     return RedirectToAction(nameof(Index));
		// }


// --------- other 

		// public IActionResult Edit(int? Id)
		// {
		//     if (Id == null || Id == 0)
		//     {
		//         return NotFound();
		//     }
		//     var cusdb = _db.Meals.Find(Id);

		//     if (cusdb == null)
		//     {
		//         return NotFound();
		//     }
		//     return View(cusdb);
		// }

		// [HttpPost]
		// [ValidateAntiForgeryToken]
		// public IActionResult Edit(Meal Item)
		// {
		//     if (ModelState.IsValid)
		//     {
		//         _db.Meals.Update(Item);
		//         _db.SaveChanges();
		//         TempData["ResultOk"] = "Data Updated Successfully !";
		//         return RedirectToAction("Index");
		//     }
		//     return View(Item);
		// }

		// public async Task<IActionResult> Delete(int? id)
		// {
		//     if (id == null || _db.Meals == null)
		//         return NotFound();
		//     var mealDb = await _db.Meals.FirstOrDefaultAsync(m => m.MealId == id);
		//     if (mealDb == null)
		//         return NotFound();
		//     return View(mealDb);
		// }

		// [HttpPost, ActionName("Delete")]
		// [ValidateAntiForgeryToken]
		// public async Task<IActionResult> DeleteConfirmed(int id)
		// {
		//     if (_db.Meals == null)
		//         return Problem("Entity set 'ApplicationDBContext.Meals'  is null.");
		//     var meal = await _db.Meals.FindAsync(id);
		//     if (meal != null)
		//         _db.Meals.Remove(meal);
		//     await _db.SaveChangesAsync();
		//     return RedirectToAction(nameof(Index));
		// }
		// private bool MealExists (int id)
		// {
		//     return (_db.Meals?.Any(e => e.MealId == id)).GetValueOrDefault();
		// }
	}
}

