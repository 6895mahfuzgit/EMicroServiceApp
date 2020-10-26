using AutoMapper;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Mapper
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<Order, OrderResponse>().ReverseMap();
        }
    }
}