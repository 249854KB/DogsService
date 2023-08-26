namespace DogsService.Dtos
{
    public class DogPublishedDto
    {
        public int Id { get; set; }
         public string Name { get; set; }
        public string Race { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int UserId { get; set; }
        public string Event { get; set; }
    }
}