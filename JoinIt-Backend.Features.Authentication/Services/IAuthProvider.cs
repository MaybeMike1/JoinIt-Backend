using JoinIt_Backend.Features.Authentication.Models.Dtos;
using JoinIt_Backend.Shared.Data;
using JoinIt_Backend.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JoinIt_Backend.Features.Authentication.Services
{
    public interface IAuthProvider
    {
        string WriteToken(Guid userGuid);
        Task<AuthenticationResponseDto> Login(AuthenticationRequestDto credentials);

        Task<AuthenticationResponseDto> Register(RegisterUserDto userDto);

        Task<AuthenticationResponseDto?> FacebookLogin(MetaRequestDto metaRequestDto);
    }

    public class AuthProvider : IAuthProvider
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ICryptService _cryptService;
        public AuthProvider(DatabaseContext databaseContext, ICryptService cryptService)
        {
            _databaseContext = databaseContext;
            _cryptService = cryptService;
        }

        public async Task<AuthenticationResponseDto?> FacebookLogin(MetaRequestDto metaRequestDto)
        {
            if (string.IsNullOrEmpty(metaRequestDto.MetaUserId))
                return null;

            var facebookUser = await _databaseContext.Users.FirstOrDefaultAsync(x => x.ExternalIdentityId.Equals(metaRequestDto.MetaUserId));

            var nameSplit = metaRequestDto.MetaUserFullName.Split(" ");
            if(facebookUser is null)
            {
                var newFacebookUser = new User { ExternalIdentityId = metaRequestDto.MetaUserId, FirstName = nameSplit[0], LastName = nameSplit[1] };
                await _databaseContext.Users.AddAsync(newFacebookUser);
                await _databaseContext.SaveChangesAsync();

                return new AuthenticationResponseDto
                {
                    Guid = newFacebookUser.Guid,
                    Token = WriteToken(newFacebookUser.Guid),
                    Message = "First Time signing in with Meta SSO - You may need to provide additional information.",
                    StatusCode = 200,
                    userDto = UserDto.MapToUserDto(newFacebookUser)
                };
            }

            return new AuthenticationResponseDto
            {
                Guid = facebookUser.Guid,
                Email = facebookUser.Email,
                Message = $"Succesfully logged in using Meta SSO - Name {metaRequestDto.MetaUserFullName}",
                Token = WriteToken(facebookUser.Guid),
                StatusCode = 200,
                userDto = UserDto.MapToUserDto(facebookUser)
            };
        }

        public async Task<AuthenticationResponseDto> Login(AuthenticationRequestDto credentials)
        {
            try
            {
                if(string.IsNullOrEmpty(credentials.Email) || string.IsNullOrEmpty(credentials.Password))
                {
                    return new AuthenticationResponseDto
                    {
                        Email = null,
                        Token = null,
                        Guid = null,
                        Message = "Email or password is invalid - please try again.",
                        StatusCode = 400,
                    };
                }
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
                        userDto = UserDto.MapToUserDto(requestedUser)

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
                await _databaseContext.Users.AddAsync(newUser);
                await _databaseContext.SaveChangesAsync();
                return new AuthenticationResponseDto
                {
                    Email = newUser.Email,
                    Guid = newUser.Guid,
                    StatusCode = 201,
                    Message = $"Registration went as expected. You are registered in the system.",
                    Token = token,
                    userDto = UserDto.MapToUserDto(newUser),
                };

            }
            catch (Exception ex)
            {
                return new AuthenticationResponseDto
                {
                    Email = userDto.Email,
                    Token = null,
                    Guid = null,
                    StatusCode = 500,
                    Message = $"Something went wrong on the server.., {ex.Message} - {ex.StackTrace}"
                };
            }
            finally
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
                Issuer = "JoinItBackend",
                Audience = "JoinItApp",
                SigningCredentials = new SigningCredentials(secretSecurityKey, SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
