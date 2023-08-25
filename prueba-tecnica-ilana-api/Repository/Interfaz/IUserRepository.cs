using AppSignalRFlutterBack.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using prueba_tecnica_ilana_api.Models.Response;
using prueba_tecnica_ilana_api.Models.Tables;

namespace Data.Services.IService
{
    public interface IUserRepository
    {
        Task<UsersRespose> Login(LoginRequest login);
        Task<IEnumerable<UsersRespose>> GetAll();
        Task<User> GetByUsers(string user);
        Task<User> GetById(int id);
        Task<bool> Create(User users);
        Task<bool> Update(User users);
        Task<bool> Delete(User users);
    }
}
