using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{

    // Product tablosu "Dal" da katmanı (data access layer)
    public interface IProductDal:IEntityRepository<Product>
    {
        List<Product> GetPaged(int page, int pageSize);
        List<Product> GetAllByCategoryId(int categoryId, int page, int pageSize);
        List<ProductDetailDto> GetProductDetails();

        List<Product> SearchProducts(string searchTerm, int page, int pageSize, int? categoryId = null);

    }
}

//Code Refactoring 