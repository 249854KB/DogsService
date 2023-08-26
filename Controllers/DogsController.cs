using System;
using System.Collections.Generic;
using AutoMapper;
using DogsService.AsyncDataServices;
using DogsService.Data;
using DogsService.Dtos;
using DogsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogsService.Controllers
{
    [Route("api/d/users/{userId}/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogRepo _repository;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;
        public DogsController(IDogRepo repository, IMapper mapper, IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
             _messageBusClient = messageBusClient;
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

            try
            {
                var dogPublishedDto = _mapper.Map<DogPublishedDto>(dogReadDto);
                dogPublishedDto.Event = "Dog_Published";
                _messageBusClient.PublishNewDog(dogPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Asynchro Error: {ex.Message}");
            }
            return CreatedAtRoute(nameof(GetDogForUser),
                new {userId = userId, dogId = dogReadDto.Id}, dogReadDto);
        }

    }
}