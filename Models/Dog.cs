using System.ComponentModel.DataAnnotations;

namespace DogsService.Models
{
    public class Dog
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Race { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}