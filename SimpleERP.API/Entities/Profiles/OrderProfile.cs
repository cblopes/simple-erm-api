using AutoMapper;
using SimpleERP.API.Models;

namespace SimpleERP.API.Entities.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<CreateOrderModel, Order>();

            CreateMap<Order, AllOrdersViewModel>()
                .ForMember(ovm => ovm.OrderStatus, options => options.MapFrom(o => (char)o.OrderStatus));

            CreateMap<Order, OrderViewModel>()
                .ForMember(ovm => ovm.OrderStatus, options => options.MapFrom(o => (char)o.OrderStatus));
        }
    }
}
