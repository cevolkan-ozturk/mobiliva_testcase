using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mobiliva.Ecommerce.Data.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobiliva.Ecommerce.Data.Domain
{
    [Table("Order", Schema = "ECommerce")]
    public class Order : BaseModel
    {

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerGsm { get; set; }

        public decimal TotalAmount { get; set; }




    }

 
}
