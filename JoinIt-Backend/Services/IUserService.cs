//using JoinIt_Backend.Models.Dtos.UserDtos;
//using JoinIt_Backend.Shared.Data;
//using Microsoft.EntityFrameworkCore;

//namespace JoinIt_Backend.Services
//{
//    public interface IUserService
//    {
//        Task<UserResponseDto> UpdateUser(Guid userGuid, UpdateUserDto updateUserDto);
//        Task<UserResponseDto> DeleteUser(Guid userGuid);

//        Task<UserResponseDto> GetUser(Guid userGuid);

//        Task<UserResponseDto> ChangePassword(Guid userGuid, UpdateUserPasswordDto updateUserPasswordDto);
//    }
//    public class UserService : IUserService
//    {
//        private readonly DatabaseContext _databaseContext;
//        private readonly ICryptService _cryptService;

//        public UserService(DatabaseContext databaseContext, ICryptService cryptService)
//        {
//            _databaseContext = databaseContext;
//            _cryptService = cryptService;
//        }

//        public async Task<UserResponseDto> ChangePassword(Guid userGuid, UpdateUserPasswordDto updateUserPasswordDto)
//        {
//            try
//            {
//                var currentUser = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Guid == userGuid);
//                if(currentUser is not null)
//                {
//                    var currentPasswordIsValid = _cryptService.Compare(currentUser.PasswordHash, updateUserPasswordDto.CurrentPassword);
//                    if(currentPasswordIsValid)
//                    {
//                        var newPasswordHash = _cryptService.HashPassword(updateUserPasswordDto.NewPassword);
//                        currentUser.PasswordHash = newPasswordHash;
//                        await _databaseContext.SaveChangesAsync();
//                        var updatedUser = _databaseContext.Users.Entry(currentUser).Entity;
//                        return new UserResponseDto
//                        {
//                            Message = $"User with Guid: {userGuid} - Password was succesfully updated.",
//                            StatusCode = 201,
//                            User = updatedUser,
//                        };
//                    }

//                    return new UserResponseDto
//                    {
//                        Message = $"User with Guid: {userGuid} - Current password doesn't match password in system.",
//                        StatusCode = 400,
//                        User = null,
//                    };
//                }
//                return new UserResponseDto
//                {
//                    Message = $"User with Guid : {userGuid} was not found",
//                    StatusCode = 404,
//                    User = null,
//                };
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message + " " + ex.StackTrace);
//                return new UserResponseDto
//                {
//                    Message = "Something went wrong on the server",
//                    User = null,
//                    StatusCode = 500
//                };
//            }
//            finally
//            {
//                await _databaseContext.DisposeAsync();
//            }
//        }

//        public async Task<UserResponseDto> DeleteUser(Guid userGuid)
//        {
//            try
//            {
//                var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Guid == userGuid);
//                if (user is not null)
//                {
//                    var deletedUser = _databaseContext.Users.Remove(user).Entity;
//                    await _databaseContext.SaveChangesAsync();
//                    return new UserResponseDto
//                    {
//                        User = deletedUser,
//                        StatusCode = 201,
//                        Message = $"User with Guid : {userGuid} has been succesfully deleted from the system."

//                    };
//                }

//                return new UserResponseDto
//                {
//                    User = null,
//                    StatusCode = 404,
//                    Message = $"Unable to find user with Guid : {userGuid}. Please check the Guid and ensure that you is stored in system.."
//                };
//            }
//            catch (Exception ex)
//            {
//                return new UserResponseDto { User = null, Message = "Something went wrong on the server.. Please try again later", StatusCode = 500 };
//            }
//            finally
//            {
//                await _databaseContext.DisposeAsync();
//            }
//        }

//        public async Task<UserResponseDto> GetUser(Guid userGuid)
//        {
//            try
//            {
//                var retsVal = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Guid == userGuid);
//                if(retsVal is not null)
//                {
//                    return new UserResponseDto
//                    {
//                        Message = $"Successfully found user with Guid {userGuid}",
//                        StatusCode = 200,
//                        User = retsVal
//                    };
//                }

//                return new UserResponseDto
//                {
//                    User = null,
//                    StatusCode = 404,
//                    Message = $"Unable to find user with Guid : {userGuid}"
//                };

//            }catch(Exception)
//            {
//                return new UserResponseDto
//                {
//                    Message = "Something went wrong on the server",
//                    User = null,
//                    StatusCode = 500
//                };
//            }
//            finally
//            {
//                await _databaseContext.DisposeAsync();
//            }
//        }

//        public async Task<UserResponseDto> UpdateUser(Guid userGuid, UpdateUserDto updateUserDto)
//        {
//            try
//            {
//                var currentUser = await _databaseContext.Users.FindAsync(userGuid);
//                if(currentUser is not null)
//                {
//                    _databaseContext.Entry(currentUser).CurrentValues.SetValues(updateUserDto);
//                    var updatedUser = _databaseContext.Entry(currentUser).Entity;
//                    return new UserResponseDto
//                    {
//                        User = updatedUser,
//                        StatusCode = 200,
//                        Message = $"User with Guid : {userGuid} was succesfully updated."
//                    };
//                }
//                return new UserResponseDto
//                {
//                    User = null,
//                    StatusCode = 404,
//                    Message = $"Unable to find User with Guid: {userGuid}"
//                };
//            }
//            catch (Exception e)
//            {
//                return new UserResponseDto
//                {
//                    Message = "Something went wrong on the server",
//                    User = null,
//                    StatusCode = 500
//                };
//            }
//            finally
//            {
//                await _databaseContext.DisposeAsync();
//            }
//        }
//    }
//}
