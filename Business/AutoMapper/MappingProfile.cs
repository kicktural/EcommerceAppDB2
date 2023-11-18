using AutoMapper;
using Entities.Concreate;
using Entities.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class MappingProfile : Profile
    {
      public MappingProfile()
        {
            CreateMap<Order, OrderCreateDTO>().ReverseMap();
        }
    }
}
