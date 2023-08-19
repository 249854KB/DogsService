using AutoMapper;
using DogsService.Data;
using DogsService.Dtos;
using DogsService.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DogsService.Controllers
{
    [Route("api/d/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IDogRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(IDogRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetUsers()
        {
            Console.WriteLine("-->> Getting User From Dog service");
            var userItems = _repository.GetAllUsers();
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(userItems));
        }
        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inboud POST # Command Service");
            return Ok("Inmbound test ok for dogs controller");
        }
        //Https and grcp is synchronius
    }
}