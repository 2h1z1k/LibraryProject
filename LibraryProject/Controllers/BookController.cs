using LibraryProject_DataAccess.Repository.IRepository;
using LibraryProject_Models;
using LibraryProject_Models.ViewModels;
using LibraryProject_Utility;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IBookRepository bookRepo, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepo = bookRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> objList = _bookRepo.GetAll(includeProperties: "Author,Publisher");
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            BookVM bookVM = new BookVM()
            {
                Book = new Book(),
                AuthorSelectList = _bookRepo.GetAllDropDownList(WC.AuthorName),
                PublisherSelectList = _bookRepo.GetAllDropDownList(WC.PublisherName),
            };
            if (id == null)
            {
                return View(bookVM);
            }
            else
            {
                bookVM.Book = _bookRepo.Find(id.GetValueOrDefault());
                if (bookVM.Book == null)
                {
                    return NotFound();
                }
                return View(bookVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM bookVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (bookVM.Book.Id == 0)
                {
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    bookVM.Book.Image = fileName + extension;

                    _bookRepo.Add(bookVM.Book);

                }
                else
                {
                    var objFromDb = _bookRepo.FirstOrDefault(u => u.Id == bookVM.Book.Id, isTracking: false);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFIle = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFIle))
                        {
                            System.IO.File.Delete(oldFIle);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        bookVM.Book.Image = fileName + extension;
                    }
                    else
                    {
                        bookVM.Book.Image = objFromDb.Image;
                    }
                    _bookRepo.Update(bookVM.Book);
                }
                bookVM.AuthorSelectList = _bookRepo.GetAllDropDownList(WC.AuthorName);
                bookVM.PublisherSelectList = _bookRepo.GetAllDropDownList(WC.PublisherName);
                _bookRepo.Save();
                return RedirectToAction("Index");
            }
            return View(bookVM);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book book = _bookRepo.FirstOrDefault(u => u.Id == id, includeProperties: "Author,Publisher");
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _bookRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
            var oldFIle = Path.Combine(upload, obj.Image);
            if (System.IO.File.Exists(oldFIle))
            {
                System.IO.File.Delete(oldFIle);
            }
            _bookRepo.Remove(obj);
            _bookRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
