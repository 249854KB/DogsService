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
            CreateMap<DogCreateDto,DogPublishedDto>();
            CreateMap<UserPublishedDto, User>()
                .ForMember(destination =>destination.ExternalID, opt => opt.MapFrom(source => source.Id));
            CreateMap<GrpcUserModel, User>()
            .ForMember(destination => destination.ExternalID, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Dogs, opt =>opt.Ignore());
            CreateMap<Dog,GrpcDogModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src=>src.Id))
                .ForMember(dest => dest.DateOfBirth,opt => opt.MapFrom(src=>src.DateOfBirth))
                .ForMember(dest => dest.Race, opt => opt.MapFrom(src=>src.Race))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src=>src.UserId));;


        }
    }
}