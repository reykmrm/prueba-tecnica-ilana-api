using AppSignalRFlutterBack.Models.DTO.Request;
using Data.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using prueba_tecnica_ilana_api.IService;
using prueba_tecnica_ilana_api.Models.Response;
using prueba_tecnica_ilana_api.Repository.Repository;

namespace AppSignalRFlutterBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersServices;

        public UsersController(IUserService usersServices)
        {
            _usersServices = usersServices;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginRequest users)
        {
            return await _usersServices.Login(users);
        }


        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<UsersRespose>> GetAll()
        {
            return await _usersServices.GetAll();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return await _usersServices.GetById(id);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Create(UsersRequest users)
        {
            return await _usersServices.Create(users);
        }

        //[Authorize]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _usersServices.Delete(id);
        }

        //[Authorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(UsersRequest user)
        {
            return await _usersServices.Update(user);
        }
    }
}
