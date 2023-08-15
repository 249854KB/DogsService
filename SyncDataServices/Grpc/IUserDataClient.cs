using System.Collections.Generic;
using DogsService.Models;

namespace DogsService.SyncDataServices.Grpc
{
    public interface IUserDataClient
    {
        IEnumerable<User> ReturnAllUsers();
    }
}