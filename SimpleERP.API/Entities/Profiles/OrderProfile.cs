using AutoMapper;
using SimpleERP.API.Models;

namespace SimpleERP.API.Entities.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<CreateOrderViewModel, Order>();
        }
    }
}
