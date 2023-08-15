using System;
using System.Collections.Generic;
using AutoMapper;
using DogsService.Data;
using DogsService.Dtos;
using DogsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogsService.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogRepo _repository;
        private readonly IMapper _mapper;

        public DogsController(IDogRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DogReadDto>> GetDogsForUser(int userId)
        {
            Console.WriteLine($"--> Hit GetDogsForUser: {userId}");

            if (!_repository.UserExists(userId))
            {
                return NotFound();
            }

            var dogs = _repository.GetDogsForUser(userId);

            return Ok(_mapper.Map<IEnumerable<DogReadDto>>(dogs));
        }

        [HttpGet("{dogId}", Name = "GetDogForUser")]
        public ActionResult<DogReadDto> GetDogForUser(int userId, int dogId)
        {
            Console.WriteLine($"--> Hit GetDogForUser: {userId} / {dogId}");

            if (!_repository.UserExists(userId))
            {
                return NotFound();
            }

            var dog = _repository.GetDog(userId, dogId);

            if(dog == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DogReadDto>(dog));
        }

        [HttpPost]
        public ActionResult<DogReadDto> CreateDogForUser(int userId, DogCreateDto dogDto)
        {
             Console.WriteLine($"--> Hit CreateDogForUser: {userId}");

            if (!_repository.UserExists(userId))
            {
                return NotFound();
            }

            var dog = _mapper.Map<Dog>(dogDto);

            _repository.CreateDog(userId, dog);
            _repository.SaveChanges();

            var dogReadDto = _mapper.Map<DogReadDto>(dog);

            return CreatedAtRoute(nameof(GetDogForUser),
                new {userId = userId, dogId = dogReadDto.Id}, dogReadDto);
        }

    }
}