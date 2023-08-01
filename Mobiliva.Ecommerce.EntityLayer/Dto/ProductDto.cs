using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobiliva.Ecommerce.EntityLayer.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public String Description { get; set; }
        public String Category { get; set; }

        public int Unit { get; set; }

        public decimal UnitPrice { get; set; }

    }
}
