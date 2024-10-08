using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Concrete.ProductManager;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>>GetAll();

        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);

        IDataResult<List<ProductDetailDto>> GetProductDetails();

        IDataResult<Product> GetById(int productId);

        IResult Add(ProductAddDto product);
        IResult Update(Product product);

        IResult AddTransactionalTest(Product product);

        IDataResult<ProductPagedListDto> GetPaged(int page, int pageSize);

        IDataResult<ProductPagedListDto> GetAllByCategoryId(int categoryId, int page, int pageSize);

        List<Product> SearchProducts(string searchTerm, int page, int pageSize, int? categoryId = null);

    }
}
