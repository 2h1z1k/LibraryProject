using LibraryProject_DataAccess.Repository.IRepository;
using LibraryProject_Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorController(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Author> objList = _authorRepo.GetAll();
            return View(objList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author obj)
        {
            if (ModelState.IsValid)
            {
                _authorRepo.Add(obj);
                _authorRepo.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _authorRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author obj)
        {
            if (ModelState.IsValid)
            {
                _authorRepo.Update(obj);
                _authorRepo.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _authorRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost (int? id)
        {
            var obj = _authorRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            _authorRepo.Remove(obj);
            _authorRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
