using Services.Shared;
using Services.User.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.User.Application.Register
{
    public interface IUserRegister
    {
        public Task<DataResponse> Register(UserRegisterRequest request);
    }
    public class UserRegister : IUserRegister
    {
        private readonly IUserRepository _repository;
        public UserRegister(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<DataResponse> Register(UserRegisterRequest request)
        {
            var result = await _repository.SaveUser(request);

            if(!result.Succeeded)
            {
                return new DataResponse() { Code=400, Message = result.ToString(), Error=true };
            }

            return new DataResponse() { Code = 201, Message = "", Error = false };
        }
    }
}
