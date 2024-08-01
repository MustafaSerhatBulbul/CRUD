using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class MusteriIslemleriController : Controller
    {
        private readonly FakeData _context;

        public MusteriIslemleriController(FakeData context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var musteriler = _context.Musteriler.ToList();
            return View(musteriler);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                _context.Musteriler.Add(musteri);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(musteri);
        }

        public IActionResult Edit(int id)
        {
            var musteri = _context.Musteriler.Find(id);
            if (musteri == null)
            {
                return NotFound();
            }
            return View(musteri);
        }

        [HttpPost]
        public IActionResult Edit(int id, Musteri musteri)
        {
            if (id != musteri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(musteri);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(musteri);
        }

        public IActionResult Delete(int id)
        {
            var musteri = _context.Musteriler.Find(id);
            if (musteri == null)
            {
                return NotFound();
            }
            return View(musteri);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var musteri = _context.Musteriler.Find(id);
            _context.Musteriler.Remove(musteri);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}






