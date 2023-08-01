using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mobiliva.Ecommerce.EntityLayer.Dto;
using Mobiliva.Ecommerce.Data.Domain;

namespace Mobiliva.Ecommerce
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDto>();
        }

    }
}
