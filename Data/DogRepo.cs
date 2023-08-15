using DogsService.Models;

namespace DogsService.Data
{
    public class DogRepo : IDogRepo
    {
        private readonly AppDbContext _context;

        public DogRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateDog(int userId, Dog dog)
        {
            if(dog == null)
            {
                throw new ArgumentNullException(nameof(dog));
            }
            dog.UserId = userId;
            _context.Dogs.Add(dog);

        }

        public void CreateUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
        }

        public bool ExternalUserExists(int externalUserId)
        {
             return _context.Users.Any(u => u.ExternalID == externalUserId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public Dog GetDog(int userId, int dogId)
        {
            return _context.Dogs
                .Where(f => f.UserId == userId && f.Id == dogId).FirstOrDefault();
        }

        public IEnumerable<Dog> GetDogsForUser(int userId)
        {
            return _context.Dogs.Where(d=> d.UserId == userId)
            .OrderBy(d=>d.User.Name);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }
    }
}