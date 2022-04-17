using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PersistenceDbContext;
using Services.Shared;
using Services.User.Application.Login;
using Services.User.Application.Register;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.User.Infraestructure
{
    public interface IUserRepository
    {
        public Task<IdentityResult> SaveUser(UserRegisterRequest request);
        public Task<IdentityAccess> LoginUser(UserLoginRequest request);
    }
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<Models.User> _userManager;
        private readonly SignInManager<Models.User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public UserRepository(UserManager<Models.User> userManager, SignInManager<Models.User> signInManager, ApplicationDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
        }
        public async Task<IdentityResult> SaveUser(UserRegisterRequest request)
        {
            var entry = new Models.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            return await _userManager.CreateAsync(entry, request.Password);
        }

        public async Task<IdentityAccess> LoginUser(UserLoginRequest request)
        {
            var result = new IdentityAccess();

            var user = await _context.Users.SingleAsync(x => x.Email == request.UserName);
            var response = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if(response.Succeeded)
            {
                result.Succeeded = true;
                result.code = 200;

                await GenerateToken(user, result);
            }
            else
            {
                result.code = 400;
                result.Message = response.ToString();
            }

            return result;
        }

        public async Task GenerateToken(Models.User user, IdentityAccess identity)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            identity.AccessToken = tokenHandler.WriteToken(createdToken);
        }
    }
}
