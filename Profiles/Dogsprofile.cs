using AutoMapper;
using DogsService.Models;
using DogsService.Dtos;
using UserService;

namespace DogsService.Profiles
{
    public class DogsProfile : Profile
    {
        public DogsProfile()
        {
            //Source -> target
            CreateMap<User, UserReadDto>();
            CreateMap<DogCreateDto, Dog>();
            CreateMap<Dog, DogReadDto>();
            CreateMap<UserPublishedDto, User>()
                .ForMember(destination =>destination.ExternalID, opt => opt.MapFrom(source => source.Id));
            CreateMap<GrpcUserModel, User>()
            .ForMember(destination => destination.ExternalID, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Dogs, opt =>opt.Ignore());


        }
    }
}