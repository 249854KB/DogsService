using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using DogsService.Data;

namespace DogsService.SyncDataServices.Grpc
{
    public class GrpcDogService : GrpcDog.GrpcDogBase
    {
        private readonly IDogRepo _repository;
        private readonly IMapper _mapper;

        public GrpcDogService(IDogRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<DogResponse> GetAllDogs(GetAllRequestDog request, ServerCallContext context)
        {
            var response = new DogResponse();
            var dogs = _repository.GetAllDogs();

            foreach(var plat in dogs)
            {
                response.Dog.Add(_mapper.Map<GrpcDogModel>(plat));
            }

            return Task.FromResult(response);
        }
    }
}