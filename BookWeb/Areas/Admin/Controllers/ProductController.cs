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
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _HostEnvironment;
        public ProductController(IUnitOfWork UnitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _UnitOfWork = UnitOfWork;
            _HostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        

        //get
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Products = new(),
                CategoryList = _UnitOfWork.Category.GetAll().Select(
                i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _UnitOfWork.CoverType.GetAll().Select(
                  i => new SelectListItem
                  {
                      Text = i.Name,
                      Value = i.Id.ToString()
                  }),
            };


            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Products = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }
         
            
            return View(productVM);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM Obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootpath = _HostEnvironment.WebRootPath;
                if (file != null)
                {
                    string FileName = Guid.NewGuid().ToString();
                    string Upload = Path.Combine(wwwRootpath,@"Image\Product");
                    string extantion = Path.GetExtension(file.FileName);

                    if(Obj.Products.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootpath, Obj.Products.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var FileStreams = new FileStream(Path.Combine(Upload, FileName + extantion), FileMode.Create))
                    {
                        file.CopyTo(FileStreams);
                    }
                    Obj.Products.ImageUrl = @"\Image\Product\" + FileName + extantion;
                }
                if (Obj.Products.Id == 0)
                {
                    _UnitOfWork.Product.Add(Obj.Products);
                }
                else
                {
                    _UnitOfWork.Product.Update(Obj.Products);
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
            var ProductList = _UnitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new { data = ProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var Obj = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error While deleting" });
            }
            var oldImagePath = Path.Combine(_HostEnvironment.WebRootPath, Obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _UnitOfWork.Product.Remove(Obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Delect Successful" });

        }
        #endregion

    }
}
