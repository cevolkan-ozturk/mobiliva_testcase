using Mobiliva.Ecommerce.Data.Context;
using Mobiliva.Ecommerce.Data.Domain;
using Mobiliva.Ecommerce.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobiliva.Ecommerce.Data
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(EcommerceEfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
