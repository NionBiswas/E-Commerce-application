using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _UnitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork UnitOfWork)
        {
            _logger = logger;
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> ProductList = _UnitOfWork.Product.GetAll(includeProperties: "Category,CoverType"); 
            return View(ProductList);
        }
        public IActionResult Details(int productId)
        {
            ShoopingCart CartObj = new()
            {
                Count= 1,
                ProductId = productId,
                Product = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category,CoverType")
            };
            
            return View(CartObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoopingCart shoopingCart)
        {
            var claimsIdetity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdetity.FindFirst(ClaimTypes.NameIdentifier);
            shoopingCart.ApplicationUserId = claim.Value;

            ShoopingCart CartFromDb = _UnitOfWork.ShoppingCart.GetFirstOrDefault(u => u.ApplicationUserId == claim.Value && u.ProductId == shoopingCart.ProductId);

            if(CartFromDb == null)
            {
                _UnitOfWork.ShoppingCart.Add(shoopingCart);
            }
            else
            {
                _UnitOfWork.ShoppingCart.IncrementCount(CartFromDb, shoopingCart.Count);
            }

           
            _UnitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}