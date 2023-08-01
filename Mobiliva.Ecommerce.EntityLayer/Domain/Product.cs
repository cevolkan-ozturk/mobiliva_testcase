using Mobiliva.Ecommerce.Data.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobiliva.Ecommerce.Data.Domain
{
    [Table("Product", Schema = "ECommerce")]
    public class Product : BaseModel
    {
        public string Description { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public int Unit { get; set; }

        public double UnitPrice { get; set; }

        public bool Status { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }


    }
}
