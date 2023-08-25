using AppSignalRFlutterBack.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using prueba_tecnica_ilana_api.Models.Response;

namespace prueba_tecnica_ilana_api.IService
{
    public interface IUserService
    {
        Task<IActionResult> Login(LoginRequest user);
        Task<IEnumerable<UsersRespose>> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Create(UsersRequest users);
        Task<IActionResult> Update(UsersRequest users);
        Task<IActionResult> Delete(int id);

    }
}
