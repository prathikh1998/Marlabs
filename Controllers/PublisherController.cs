using EF_DataAccess.Data;
using EF_Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace MDemo.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PublisherController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Publisher> publishers = _db.Publishers.ToList();
            return View(publishers);
        }

        public IActionResult Upsert(int? id)
        {
            Publisher publisher = new();
            if (id == null || id == 0)
            {
                return View(publisher);
            }
            publisher = _db.Publishers.First(u => u.Publisher_Id == id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Publisher_Id == 0)
                {
                    //create
                    await _db.Publishers.AddAsync(obj);
                }
                else
                {
                    _db.Publishers.Update(obj);

                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            Publisher publisher = new();
            publisher = _db.Publishers.First(u => u.Publisher_Id == id);
            if (publisher == null)
            {
                return NotFound();

            }
            _db.Publishers.Remove(publisher);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
