using JoinIt_Backend.Models;
using JoinIt_Backend.Models.Dtos;
using JoinIt_Backend.Models.Dtos.UserDtos;
using JoinIt_Backend.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JoinIt_Backend.Services
{
    public interface IAuthProvider
    {
        string WriteToken(Guid userGuid);
        Task<AuthenticationResponseDto> Login(AuthenticationRequestDto credentials);

        Task<AuthenticationResponseDto> Register(RegisterUserDto userDto);
    }

    internal sealed class AuthProvider : IAuthProvider
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ICryptService _cryptService;
        public AuthProvider(DatabaseContext databaseContext, ICryptService cryptService)
        {
            _databaseContext = databaseContext;
            _cryptService = cryptService;
        }

        public async Task<AuthenticationResponseDto> Login(AuthenticationRequestDto credentials)
        {
            try
            {
                var requestedUser = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Email == credentials.Email);
                var isVerified = requestedUser != null && _cryptService.Compare(requestedUser.PasswordHash, credentials.Password);

                if (isVerified && requestedUser != null)
                {
                    var token = WriteToken(requestedUser.Guid);
                    return new AuthenticationResponseDto
                    {
                        Email = credentials.Email,
                        Token = token,
                        Guid = requestedUser.Guid,
                        Message = "User was sucessfully logged in.",
                        StatusCode = 200,

                    };
                }

                return new AuthenticationResponseDto
                {
                    Email = credentials.Email,
                    Token = null,
                    Guid = null,
                    Message = "Email or password is invalid - please try again.",
                    StatusCode = 400,
                };
            }
            catch (Exception e)
            {
                return new AuthenticationResponseDto
                {
                    Email = credentials.Email,
                    Token = null,
                    Guid = null,
                    Message = $"Something went wrong on the server. Unable to provide token. (Message, Stacktrace) - ({e.Message},{e.StackTrace})",
                    StatusCode = 500,
                };
            }
        }

        public async Task<AuthenticationResponseDto> Register(RegisterUserDto userDto)
        {
            try
            {
                var userExists = await _databaseContext.Users.AnyAsync(x => x.Email == userDto.Email);

                var newUser = new User
                {
                    FirstName = userDto.FirstName,
                    Username = userDto.Username,
                    Email = userDto.Email,
                    PasswordHash = _cryptService.HashPassword(userDto.PlainPassword),
                    PhoneNumber = userDto.Phonenumber,
                    SignUpDate = DateTime.Now,
                    LastName = userDto.LastName ?? string.Empty,
                };

                if (userExists)
                {
                    return new AuthenticationResponseDto
                    {
                        Email = null,
                        Token = null,
                        Guid = null,
                        Message = $"Email is already registered in the system, please log in with following email or use another mail.",
                        StatusCode = 400,
                    };
                }

                var token = WriteToken(newUser.Guid);
                var some = await _databaseContext.AddAsync<User>(newUser, new CancellationToken());
                await _databaseContext.SaveChangesAsync();
                return new AuthenticationResponseDto
                {
                    Email = newUser.Email,
                    Guid = newUser.Guid,
                    StatusCode = 201,
                    Message = $"Registration went as expected. You are registered in the system.",
                    Token = token,
                };

            }
            catch(Exception ex)
            {
                return new AuthenticationResponseDto
                {
                    Email = userDto.Email,
                    Token = null,
                    Guid = null,
                    StatusCode = 500,
                    Message = $"Something went wrong on the server.., {ex.Message} - {ex.StackTrace}"
                };
            }finally
            {
                await _databaseContext.DisposeAsync();
            }
        }

        public string WriteToken(Guid userGuid)
        {
            var secret = "this is my custom Secret key for authentication";
            var secretSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userGuid.ToString())
                }),
                Issuer = "test",
                Audience = "test",
                SigningCredentials = new SigningCredentials(secretSecurityKey, SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
