using LibraryProject_DataAccess.Repository.IRepository;
using LibraryProject_Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherRepository _publisherRepo;

        public PublisherController(IPublisherRepository publisherRepo)
        {
            _publisherRepo = publisherRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Publisher> objList = _publisherRepo.GetAll();
            return View(objList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                _publisherRepo.Add(obj);
                _publisherRepo.Save();
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
            var obj = _publisherRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                _publisherRepo.Update(obj);
                _publisherRepo.Save();
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
            var obj = _publisherRepo.Find(id.GetValueOrDefault());
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
            var obj = _publisherRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            _publisherRepo.Remove(obj);
            _publisherRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
