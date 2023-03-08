using BookWeb.DataAccess.Repository.IRepository;
using MediaBrowser.Model.Activity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbConcext _db;
        private ICategoryRepository _categoryRepository;
        private ICoverTypeRepository _coverTypeRepository;
        private IProductRepository _productRepository;
        private ICompanyRepository _companyRepository;
        private IApplicationUserRepository _applicationRepository;
        private IShoppingCartRepository _shoppingCartRepository;
        private IOrderHeaderRepository _orderHeaderRepository;
        private IOrderDetailsRepository _orderDetailsRepository;

        public UnitOfWork(ApplicationDbConcext db)
        {
            _db = db;
   
        //Category = new CategoryRepository(_db);
        //CoverType = new CoverTypeRepository(_db);
        //Product = new ProductRepository(_db);

    }
        public ICategoryRepository Category => _categoryRepository = _categoryRepository ?? new CategoryRepository(_db);
        public ICoverTypeRepository CoverType => _coverTypeRepository = _coverTypeRepository ?? new CoverTypeRepository(_db);
        public IProductRepository Product => _productRepository = _productRepository ?? new ProductRepository(_db);
        public ICompanyRepository Company => _companyRepository = _companyRepository ?? new CompanyRepository(_db);
        public IShoppingCartRepository ShoppingCart => _shoppingCartRepository= _shoppingCartRepository?? new ShoppingCartRepository(_db);
        public IApplicationUserRepository ApplicationUser => _applicationRepository = _applicationRepository ?? new ApplicationUserRepository(_db);
        public IOrderHeaderRepository OrderHeader => _orderHeaderRepository = _orderHeaderRepository ?? new OrderHeaderRepository(_db);
        public IOrderDetailsRepository OrderDetails => _orderDetailsRepository = _orderDetailsRepository ?? new OrderDetailRepository(_db);


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
