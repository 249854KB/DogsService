using DogsService.Dtos;

namespace DogsService.SyncDataServices.Http
{
    public interface IPhotoDataClient
    {
        Task SendDogToPhoto(DogReadDto dog);
    }
}