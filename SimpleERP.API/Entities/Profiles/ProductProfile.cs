using AutoMapper;
using SimpleERP.API.Entities;
using SimpleERP.API.Models;

namespace SimpleERP.API.Entities.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductModel, Product>();

            CreateMap<AlterProductModel, Product>();

            CreateMap<Product, ProductViewModel>();
        }
    }
}
