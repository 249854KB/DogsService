using System.ComponentModel.DataAnnotations;

namespace DogsService.Dtos
{
    public class DogCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Race { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        
    }
}