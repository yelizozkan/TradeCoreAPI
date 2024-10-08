using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //[SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        //Attribute-anlam yükleme 
        public IResult Add(ProductAddDto productAddDto)
        {
            //Aynı isimde ürün eklenemez
            //business codes
            var product = new Product()
            {
                ProductName = productAddDto.ProductName,
                CategoryId = productAddDto.CategoryId,
                UnitPrice = productAddDto.UnitPrice,
                UnitsInStock = productAddDto.UnitsInStock,
            };

            IResult result=BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductNameExists(product.ProductName),CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }

       
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);

        }

        [CacheAspect] //key,value
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 6) 
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice>=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }


        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            var reuslt = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;

            if (reuslt >= 50)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }

            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //Select count(*) from products where CategoryId=1
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;

            if (result >= 50)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            //Select count(*) from products where CategoryId=1
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();

            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 50)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        //[TransactionScopeAspect]
        //public IResult AddTransactionalTest(Product product)
        //{
        //    Add(product);
        //    if (product.UnitPrice < 10)
        //    {
        //        throw new Exception("");
        //    }

        //    Add(product);
        //    return null;
        //}


        public IDataResult<ProductPagedListDto> GetPaged(int page, int pageSize)
        {
            var totalCount = _productDal.GetAll().Count;
            var products = _productDal.GetPaged(page, pageSize);

            var result = new ProductPagedListDto
            {
                Products = products,
                TotalCount = totalCount
            };

            return new SuccessDataResult<ProductPagedListDto>(result);
        }

        public IDataResult<ProductPagedListDto> GetAllByCategoryId(int categoryId, int page, int pageSize)
        {
            var query = _productDal.GetAll(p => p.CategoryId == categoryId);
            var totalCount = query.Count();
            var products = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            var result = new ProductPagedListDto
            {
                Products = products,
                TotalCount = totalCount
            };  

            return new SuccessDataResult<ProductPagedListDto>(result);
        }

        public List<Product> SearchProducts(string searchTerm, int page, int pageSize, int? categoryId = null)
        {
            return _productDal.SearchProducts(searchTerm, page, pageSize, categoryId);
        }

        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
