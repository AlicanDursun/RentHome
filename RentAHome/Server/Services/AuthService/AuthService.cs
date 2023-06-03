using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RentAHome.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return int.Parse(
             _httpContextAccessor.HttpContext.User
             .FindFirstValue(ClaimTypes.NameIdentifier));
            }
            return 0;
        }

        public async Task<ServiceResponse<string>> RegisterOrLogin(User user)
        {
            var response = new ServiceResponse<string>();
            var existsUser = await UserExists(user);
            if (existsUser != null)
            {
                if (!VerifyPasswordHash(user.Password, existsUser.PasswordHash, existsUser.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Email or Password Wrong!";

                }
                else
                {
                    response.Data = CreateToken(existsUser);
                }
            }
            else if (await EmailExists(user.Email))
            {
                response.Success = false;
                response.Message = "Email already Exists";
            }
            else
            {
                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                response.Data = CreateToken(user);

            }
            return response;
        }
        public async Task<User> UserExists(User user)
        {
            return await _context.Users.FirstOrDefaultAsync(w => w.Email.ToLower()
             .Equals(user.Email.ToLower()) && w.Password.ToLower().Equals(user.Password.ToLower()));

        }
        public async Task<bool> EmailExists(string email)
        {
            if (await _context.Users.AnyAsync(w => w.Email.ToLower()
             .Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }


    }
}
