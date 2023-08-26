using DogsService.Dtos;

namespace DogsService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewDog(DogPublishedDto dogPublishedDto);
    }
}