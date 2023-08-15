namespace DogsService.Dtos
{
    public class DogReadDto
    {
        public int Id { get; set; }
         public string Name { get; set; }
        public string Race { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int UserId { get; set; }
    }
}