using AutoMapper;
using SimpleERP.API.Models;

namespace SimpleERP.API.Entities.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile() 
        { 
            CreateMap<CreateOrderItemViewModel, OrderItem>();

            CreateMap<AlterOrderItemViewModel, OrderItem>();
        }
    }
}
