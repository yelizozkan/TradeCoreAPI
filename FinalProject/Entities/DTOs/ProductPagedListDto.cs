using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductPagedListDto
    {
        public List<Product> Products { get; set; }
        public int TotalCount { get; set; }
    }
}
