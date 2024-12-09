using Microsoft.AspNetCore.Mvc;
using MVC_DEMO.Data;
using MVC_DEMO.Models;

namespace MVC_DEMO.Controllers
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
            List<Category> categories = new();
            categories = _db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.CategoryName == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CategoryName", "Both cant be equal");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category categories = _db.Categories.Find(id);
            if(categories == null)
            {
                return NotFound();
            }
            return View(categories);
           
        }

        [HttpPost]
        public IActionResult Upsert(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View();
            
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category categories = _db.Categories.FirstOrDefault(u => u.Category_Id == Id);
            if(categories == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(categories);
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
    }

}
