using AppSignalRFlutterBack.Models.DTO.Request;
using Data.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using prueba_tecnica_ilana_api.AuxM;
using prueba_tecnica_ilana_api.IService;
using prueba_tecnica_ilana_api.Models.Response;
using prueba_tecnica_ilana_api.Models.Tables;
using prueba_tecnica_ilana_api.Repository.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace prueba_tecnica_ilana_api.Services
{
  
    public class UsersServices : ControllerBase, IUserService
    {
        private readonly IUserRepository _usersRepository;
        private IConfiguration _config;

        public UsersServices(IUserRepository usersRepository, IConfiguration config)
        {
            _usersRepository = usersRepository;
            _config = config;
        }

        public async Task<IActionResult> Login(LoginRequest user)
        {
            var claveEncriptada = Encriptar.EncriptarPas(user.Pass);
            user.Pass = claveEncriptada;
            UsersRespose users = await _usersRepository.Login(user);
            if (users.Name == null)
            {
                return BadRequest(new { result = "Datos invalidos", isSuces = false });
            }


            string token = generateToken(users);

            return Ok(new {token = token, data=users.Name, isSuces = true });
        }

        public async Task<IEnumerable<UsersRespose>> GetAll()
        {
            return await _usersRepository.GetAll();
        }

        public async Task<User> GetByUsers(string user)
        {
            return await _usersRepository.GetByUsers(user);
        }

        public async Task<IActionResult> GetById(int id)
        {
            User users = await _usersRepository.GetById(id);
            if (users.Email == null)
            {
                return BadRequest(new { result = "Usuario no encontrado", isSuces = false });
            }

            return Ok(new {data = users, isSuces = true});
        }



        public async Task<IActionResult> Create(UsersRequest users)
        {
            try
            {
               
                User user = await GetByUsers(users.Email);

                if (user.Email != null)
                {
                    return BadRequest(new { result = "Usuario " + user.Email + " registrado previamente", isSuces = false });
                }


                var claveEncriptada = Encriptar.EncriptarPas(users.Pass);


                User userss = new User()
                {
                    Name = users.Name,
                    Email = users.Email,
                    Pass = claveEncriptada,
                };


                var resultRespository = await _usersRepository.Create(userss);
                if (resultRespository == true)
                {
                    return Ok(new { result = "Usuario registado con exito", isSuces = true });
                }

                return BadRequest(new { result = "Error al registar el usuario", isSuces = false });
            }
            catch (Exception ex)
            {
                return BadRequest(new { result = ex.Message, isSuces = false });
            }
        }

        public async Task<IActionResult> Update(UsersRequest users)
        {

            try
            {

                User user = await GetByUsers(users.Email);

                if (users.Id != users.Id)
                {
                    return BadRequest(new { result = "Usuario registrado previamente", isSuces = false });
                }

                User userss = await _usersRepository.GetById(users.Id);
                if (userss == null)
                {
                    return BadRequest(new { result = "Usuario no existe", isSuces = false });
                }


                userss.Id = users.Id;
                userss.Name = users.Name;
                userss.Email = users.Email;

                var resultRespository = await _usersRepository.Update(userss);
                if (resultRespository == true)
                {
                    return Ok(new { result = "Usuario actualizado con exito", isSuces = true });
                }

                return BadRequest(new { result = "Error al actualizar el usuario", isSuces = false });
            }
            catch (Exception ex)
            {
                return BadRequest(new { result = ex.Message, isSuces = false });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                User users = await _usersRepository.GetById(id);
                if (users.Email == null)
                {
                    return BadRequest(new { result = "Usuario no encontrado", isSuces = false });
                }

                var resultDelete = await _usersRepository.Delete(users);

                if (resultDelete == true)
                {
                    return Ok(new { result = "Usuario eliminado", isSuces = true });
                }
                return BadRequest(new { result = "No se pudo eliminar el usuario", isSuces = false });
            }
            catch (Exception ex)
            {
                return BadRequest(new { result = ex.Message, isSuces = false });
            }
        }


        private string generateToken(UsersRespose admin)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, admin.Name),
            new Claim(ClaimTypes.Email, admin.Email),
            //new Claim("AdminType", admin.AdminType),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

    }
}
