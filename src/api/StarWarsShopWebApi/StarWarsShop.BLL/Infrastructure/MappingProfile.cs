using AutoMapper;
using StarWarsShop.BLL.ViewModels;
using StarWarsShop.DAL.Entities;

namespace StarWarsShop.BLL.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Request, RequestVM>();
            //CreateMap<RequestVM, Request>();
            CreateMap<Request, RequestVM>().ReverseMap();

            //CreateMap<Customer, CustomerVM>().ReverseMap();

            //CreateMap<StatusRequest, StatusRequestVM>().ReverseMap();
        }
    }
}
