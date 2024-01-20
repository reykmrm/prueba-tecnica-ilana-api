using AppSignalRFlutterBack.Models.DTO.Request;
using Data.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using prueba_tecnica_ilana_api.IService;
using prueba_tecnica_ilana_api.Models.Response;
using prueba_tecnica_ilana_api.Repository.Repository;
using prueba_tecnica_ilana_api.Services.Interfaz;
using PruebaTecnicaTareas.Models.DTO;
using RestSharp;

namespace AppSignalRFlutterBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersServices;
        private readonly ICaptchaVerificationService _captchaVerificationService;



        public UsersController(IUserService usersServices, ICaptchaVerificationService captchaVerificationService)
        {
            _usersServices = usersServices;
            _captchaVerificationService = captchaVerificationService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginRequest users)
        {
            //var captchaValid = await _captchaVerificationService.IsCaptchaValid(users.CaptchaResponse);
            //if (!captchaValid)
            //{
            //    return BadRequest("Captcha inválido");
            //}
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


        [HttpGet("[action]")]
        public async Task<IActionResult> getTareasOtraApi()
        {
            List<TareaDTO> tareaDTO = new List<TareaDTO>();
            try
            {
                //string jsonString = System.Text.Json.JsonSerializer.Serialize(Req_SeguimientoGuia);

                var client3 = new RestClient("https://localhost:7236/api/Tarea");
                var request2 = new RestRequest(Method.GET);
                request2.AddHeader("content-type", "application/json");
                //request2.AddParameter("application/json", jsonString, ParameterType.RequestBody);
                //request2.AddHeader("authorization", $"Bearer {token}");
                IRestResponse response = client3.Execute(request2);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tareaDTO = JsonConvert.DeserializeObject<List<TareaDTO>>(response.Content);
                    return Ok(tareaDTO);
                }
                return Ok(tareaDTO);

            }

            catch (Exception)
            {
                return NotFound();
            }
            //return await _usersServices.Update(captchat);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> potTareasOtraApi(TareaDTO TareaDTO)
        {
            Resul resTareaDTO = new Resul();
            try
            {
                string jsonString = System.Text.Json.JsonSerializer.Serialize(TareaDTO);

                var client3 = new RestClient("https://localhost:7236/api/Tarea");
                var request2 = new RestRequest(Method.POST);
                request2.AddHeader("content-type", "application/json");
                request2.AddParameter("application/json", jsonString, ParameterType.RequestBody);
                //request2.AddHeader("authorization", $"Bearer {token}");
                IRestResponse response = client3.Execute(request2);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    resTareaDTO = JsonConvert.DeserializeObject<Resul>(response.Content);
                    return Ok(resTareaDTO);
                }
                return Ok(resTareaDTO);

            }

            catch (Exception)
            {
                return NotFound();
            }
            //return await _usersServices.Update(captchat);
        }
    }
}
