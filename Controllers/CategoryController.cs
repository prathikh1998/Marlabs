using EF_DataAccess.Data;
using EF_Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace MDemo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new();
            if(id == null || id == 0)
            {
                return View(category);
            }
            category = _db.Categories.First(u=> u.Category_Id == id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Category_Id == 0)
                {
                    //create
                   await _db.Categories.AddAsync(obj);
                }
                else
                {
                    _db.Categories.Update(obj);

                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            Category category = new();
            category = _db.Categories.First(u => u.Category_Id == id);
            if(category == null)
            {
                return NotFound();
                
            }
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
