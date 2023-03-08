using BookWeb.DataAccess;
using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> Categorydb = _UnitOfWork.Category.GetAll();
            return View(Categorydb);
        }
        //get
        public IActionResult Create()
        {
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category Obj)
        {
            if (Obj.Name == Obj.DisplyOrder.ToString())
            {
                ModelState.AddModelError("Name", "The dispalyOrder cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Add(Obj);
                _UnitOfWork.Save();
                TempData["Success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(Obj);

        }

        //get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var FindIdToEdit = _db.Categories.Find(id);
            var FindIdToEdit = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (FindIdToEdit == null)
            {
                return NotFound();
            }
            return View(FindIdToEdit);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category Obj)
        {
            if (Obj.Name == Obj.DisplyOrder.ToString())
            {
                ModelState.AddModelError("Name", "The dispalyOrder cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Update(Obj);
                _UnitOfWork.Save();
                TempData["Success"] = "Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(Obj);

        }

        //get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var FindIdToEdit = _db.Categories.Find(id);
            var FindIdToDelete = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (FindIdToDelete == null)
            {
                return NotFound();
            }
            return View(FindIdToDelete);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var FindIdToDelete = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (FindIdToDelete == null)
            {
                return NotFound();
            }

            _UnitOfWork.Category.Remove(FindIdToDelete);
            _UnitOfWork.Save();
            TempData["Success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

        }

    }
}
