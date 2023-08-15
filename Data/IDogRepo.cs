using DogsService.Models;

namespace DogsService.Data
{
    public interface IDogRepo
    {
        bool SaveChanges();
        IEnumerable<User> GetAllUsers();
        void CreateUser(User user);
        bool UserExists(int externalUserId);

        bool ExternalUserExists(int userId);

        IEnumerable<Dog> GetDogsForUser(int userId);
        Dog GetDog(int userId, int dogId);
        void CreateDog(int userId, Dog dog);
    }
}