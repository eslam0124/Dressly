using System.Text.RegularExpressions;
using AutoMapper;
using Dreslay.Models;

namespace Dreslay.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Tailor, TailorDto>().ReverseMap();
            CreateMap<Admin, AdminDto>().ReverseMap();
            CreateMap<Fabric, FabricDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feedback, FeedbackDto>().ReverseMap();
            CreateMap<Complaint, ComplaintDto>().ReverseMap();
            CreateMap<Favorite, FavoriteDto>().ReverseMap();
            CreateMap<Offer, OfferDto>().ReverseMap();
        }
    }
}
