using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loosely coupled
        // naming convention 
       // IoC Container  -- Inversion of Control
        IProductService _productService;


        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("getall")]
        public IActionResult GetAll ()
        {

            Thread.Sleep(1000);
            //Dependency chain 
                var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult Add(ProductAddDto productAddDto)
        {
            var result = _productService.Add(productAddDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getpaged")]
        public IActionResult GetPaged(int page = 1, int pageSize = 10)
        {
            var result = _productService.GetPaged(page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbycategoryid")]
        public IActionResult GetByCategoryId(int categoryId, int page = 1, int pageSize = 10)
        {
            var result = _productService.GetAllByCategoryId(categoryId, page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("search")]
        public IActionResult SearchProducts(string searchTerm, int page = 1, int pageSize = 10, int? categoryId = null)
        {
            var result = _productService.SearchProducts(searchTerm, page, pageSize, categoryId);
            if (result != null && result.Any())
            {
                return Ok(result);
            }
            return NotFound("Belirtilen arama terimiyle hiçbir ürün bulunamadı.");
        }




    }
}


