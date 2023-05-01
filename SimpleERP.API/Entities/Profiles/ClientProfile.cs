using AutoMapper;
using SimpleERP.API.Entities;
using SimpleERP.API.Models;

namespace SimpleERP.API.Entities.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<CreateClientModel, Client>();

            CreateMap<AlterClientModel, Client>();

            CreateMap<Client, ClientViewModel>();
        }


    }
}
