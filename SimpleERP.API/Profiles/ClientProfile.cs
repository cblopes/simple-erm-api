using AutoMapper;
using SimpleERP.API.Entities;
using SimpleERP.API.Models;

namespace SimpleERP.API.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        { 
            CreateMap<ClientInputModel, Client>();
            
            CreateMap<Client, ClientViewModel>();
        }


    }
}
