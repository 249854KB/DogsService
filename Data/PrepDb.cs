using DogsService.Models;
using DogsService.SyncDataServices.Grpc;
using System;

namespace DogsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using(var servicesScope  = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = servicesScope.ServiceProvider.GetService<IUserDataClient>();
                var users = grpcClient.ReturnAllUsers();
                SeedData(servicesScope.ServiceProvider.GetService<IDogRepo>(),users);
            }
        }
        private static void SeedData(IDogRepo repo, IEnumerable<User> users)
        {
            Console.WriteLine("Seeding new users...");

            foreach (var user in users)
            {
                if(!repo.ExternalUserExists(user.ExternalID))
                {
                    repo.CreateUser(user);
                }
                repo.SaveChanges();
            }
        }

        
    }
}