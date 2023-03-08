using BookWeb.DataAccess;
using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CoverTypeController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> Covertypedb = _UnitOfWork.CoverType.GetAll();
            return View(Covertypedb);
        }
        //get
        public IActionResult Create()
        {
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType Obj)
        {
            
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.Add(Obj);
                _UnitOfWork.Save();
                TempData["Success"] = "CoverType Created Successfully";
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
            //var FindIdToEdit = _db.CoverType.Find(id);
            var FindIdToEdit = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (FindIdToEdit == null)
            {
                return NotFound();
            }
            return View(FindIdToEdit);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType Obj)
        {
          
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.Update(Obj);
                _UnitOfWork.Save();
                TempData["Success"] = "CoverType Edited Successfully";
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
            //var FindIdToEdit = _db.CoverType.Find(id);
            var FindIdToDelete = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
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
            var FindIdToDelete = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (FindIdToDelete == null)
            {
                return NotFound();
            }

            _UnitOfWork.CoverType.Remove(FindIdToDelete);
            _UnitOfWork.Save();
            TempData["Success"] = "CoverType Deleted Successfully";
            return RedirectToAction("Index");

        }

    }
}
