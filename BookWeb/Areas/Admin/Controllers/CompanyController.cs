using BookWeb.DataAccess;
using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;
using BookWeb.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CompanyController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
          
        }
        public IActionResult Index()
        {
            return View();
        }
        

        //get
        public IActionResult Upsert(int? id)
        {

            Company company = new();


            if (id == null || id == 0)
            {
                return View(company);
            }
            else
            {
               company = _UnitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company Obj)
        {

            if (ModelState.IsValid)
            {
             
                
                if (Obj.Id == 0)
                {
                    _UnitOfWork.Company.Add(Obj);
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    _UnitOfWork.Company.Update(Obj);
                    TempData["success"] = "Company Updated successfully";
                }
            
                _UnitOfWork.Save();
                TempData["Success"] = "Product Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(Obj);

        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() 
        {
            var CompanyList = _UnitOfWork.Company.GetAll();
            return Json(new { data = CompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var Obj = _UnitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error While deleting" });
            }
            _UnitOfWork.Company.Remove(Obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Delect Successful" });

        }
        #endregion

    }
}
