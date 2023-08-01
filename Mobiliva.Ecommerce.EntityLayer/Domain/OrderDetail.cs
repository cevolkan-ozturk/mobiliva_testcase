using Mobiliva.Ecommerce.Data.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobiliva.Ecommerce.Data.Domain
{
    [Table("OrderDetail", Schema = "ECommerce")]
    public class OrderDetail : BaseModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
