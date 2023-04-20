using AutoMapper;
using SimpleERP.API.Entities;
using SimpleERP.API.Models;

namespace SimpleERP.API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<CreateProductModel, Product>();

            CreateMap<UpdateProductModel, Product>();

            CreateMap<Product, ProductViewModel>();
        }
    }
}
