using Services.Shared;
using Services.User.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.User.Application.Login
{
    public interface IUserLogin
    {
        public Task<IdentityAccess> Authenticate(UserLoginRequest request);
    }
    public class UserLogin : IUserLogin
    {
        private readonly IUserRepository _repository;

        public UserLogin(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IdentityAccess> Authenticate(UserLoginRequest request)
        {
            return await _repository.LoginUser(request);
        }
    }
}
