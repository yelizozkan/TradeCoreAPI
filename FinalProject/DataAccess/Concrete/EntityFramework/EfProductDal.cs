using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock = p.UnitsInStock
                             };
                return result.ToList();

            }
        }

        public List<Product> GetPaged(int page, int pageSize)
        {
            using (var context = new NorthwindContext())
            {
                return context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public List<Product> GetAllByCategoryId(int categoryId, int page, int pageSize)
        {
            using (var context = new NorthwindContext())
            {
                return context.Products
                    .Where(p => p.CategoryId == categoryId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public List<Product> SearchProducts(string searchTerm, int page, int pageSize, int? categoryId = null)
        {
            using (var context = new NorthwindContext())
            {
                var query = context.Products.AsQueryable();

                if (categoryId.HasValue)
                {
                    query = query.Where(p => p.CategoryId == categoryId.Value);
                }

                query = query.Where(p => p.ProductName.Contains(searchTerm) || p.Category.CategoryName.Contains(searchTerm))
                             .Skip((page - 1) * pageSize)
                             .Take(pageSize);

                
                return query.ToList();
            }
        }

    }
}
