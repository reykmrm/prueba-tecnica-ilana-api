using AppSignalRFlutterBack.Models.DTO.Request;
using Data.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using prueba_tecnica_ilana_api.Models.Response;
using prueba_tecnica_ilana_api.Models.Tables;
using System.Xml.Linq;

namespace prueba_tecnica_ilana_api.Repository.Repository
{
    public class UsersRepository: IUserRepository
    {
        public readonly PruebatecnicailanaContext _contex;
        public UsersRepository(PruebatecnicailanaContext contex)
        {
            _contex = contex;
        }

        public async Task<UsersRespose> Login(LoginRequest login)
        {
            User resul = new User();
            resul = await _contex.Users.FirstOrDefaultAsync(x => x.Email == login.Email && x.Pass == login.Pass) ?? resul;

            UsersRespose result = new UsersRespose();
            result.Id = resul.Id;
            result.Name = resul.Name;
            result.Email = resul.Email;
            result.Pass = resul.Pass;
            return result;
        }


        public async Task<IEnumerable<UsersRespose>> GetAll()
        {
            return await _contex.Users.Select(users => new UsersRespose
            {
                Id = users.Id,
                Name = users.Name,
                Email = users.Email,
                Pass = users.Pass,
            }).ToListAsync();
        }


        public async Task<User> GetByUsers(string user)
        {
            User result = new User();
            result = await _contex.Users.FirstOrDefaultAsync(x => x.Email == user) ?? result;
            return result;
        }

        public async Task<User> GetById(int id)
        {
            User result = new User();
            result = await _contex.Users.FirstOrDefaultAsync(x => x.Id == id) ?? result;
            return result;
        }

        public async Task<bool> Create(User users)
        {
            await _contex.Users.AddAsync(users);

            return await Save();
        }

        public async Task<bool> Update(User users)
        {
            _contex.Users.Update(users);

            return await Save();
        }

        public async Task<bool> Delete(User users)
        {
            //var userExist = await _contex.Users.FindAsync(users.Id);
            _contex.Users.Remove(users);

            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _contex.SaveChangesAsync() > 0 ? true : false;
        }

    }
}

