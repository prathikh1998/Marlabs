using EF_DataAccess.Data;
using EF_Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace MDemo.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Author> author = _db.Authors.ToList(); 
            return View(author);
        }

        public IActionResult Upsert(int? id)
        {
            Author author = new();
            if (id == null || id == 0)
            {
                return View(author);
            }
            author = _db.Authors.First(u => u.Author_Id == id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Author obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Author_Id == 0)
                {
                    //create
                    await _db.Authors.AddAsync(obj);
                }
                else
                {
                    _db.Authors.Update(obj);

                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }


        public async Task<IActionResult> Delete(int? id)
        { 
            Author author = new();
            author = _db.Authors.First(u => u.Author_Id == id);
            if (author == null)
            {
                return NotFound();

            }
            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }

}
