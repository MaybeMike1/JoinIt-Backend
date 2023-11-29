using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoinIt_Backend.Features.Activity.Models.Dtos;
using JoinIt_Backend.Shared.Data;
using JoinIt_Backend.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace JoinIt_Backend.Features.Activity.Services
{
    public interface IActivityService
    {
        Task<ActivityResponseDto> GetActivities();

        Task<ActivityResponseDto> CreateActivity(CreateActivityDto createActivityDto, Guid creatorGuid);

        Task<ActivityResponseDto> GetUserActivities(Guid userGuid);

        Task<ActivityResponseDto> EnrollActivity(Guid userGuid, Guid activityGuid);

        Task<ActivityResponseDto> DetachActivity(Guid userGuid, Guid activityGuid);

    }

    public class ActivityService : IActivityService
    {
        private readonly DatabaseContext _databaseContext;

        public ActivityService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<ActivityResponseDto> CreateActivity(CreateActivityDto createActivityDto, Guid creatorGuid)
        {
            try
            {
                _databaseContext.Database.BeginTransaction();
                var activityCreator = await _databaseContext.Users.FirstAsync(x => x.Guid == creatorGuid);
                if (activityCreator is null)
                {
                    return new ActivityResponseDto
                    {
                        StatusCode = 400,
                        Message = "Unable to find logged in user, currently creating activity.",
                        Activities = null,
                        NewActivity = null,
                    };
                }

                var address = await _databaseContext.Addresses.FirstAsync(x => x.Guid == createActivityDto.AddressGuid);
                Shared.Models.Activity activity = CreateActivityDto.MapToActivity(createActivityDto);
                activity.Address = address;
                await _databaseContext.Activities.AddAsync(activity);

                var initialAttendance = new Attendance { UserId = activityCreator.Guid!, ActivityId = activity.Guid, User = activityCreator!, Activity = activity };
                activity.Attendants.Add(initialAttendance);
                await _databaseContext.SaveChangesAsync();


                var refreshedActivity = await _databaseContext.Activities.Select(x => new ActivityDto
                {
                    ActivityGuid = x.Guid,
                    ActivityName = x.Name,
                    Attendants =  x.Attendants.Select(x => x.UserId).ToList(),
                    ActivityType = x.ActivityType.Type,
                    ActivityTypeDescription = x.ActivityType.Description,
                    FriendlyStartDate = x.Date.ToString("dddd, MMMM dd, yyyy h:mm", new CultureInfo("da-DK")),
                    Location = String.Format("{0}", "", x.Address.StreetNumber.ToString()),
                }).ToListAsync();

                _databaseContext.Database.CommitTransaction();

                return new ActivityResponseDto
                {
                    StatusCode = 200,
                    Message = "Succesfully add new Activity",
                    Activities = refreshedActivity,
                    NewActivity = new ActivityDto(),
                };
                
            }
            catch (Exception e)
            {
                return new ActivityResponseDto
                {
                    StatusCode = 500,
                    Activities = null,
                    NewActivity = null,
                    Message = String.Format("{0} \n {1}", e.Message, e.StackTrace)
                };
            }
        }

        public Task<ActivityResponseDto> DetachActivity(Guid userGuid, Guid activityGuid)
        {
            throw new NotImplementedException();
        }

        public async Task<ActivityResponseDto> EnrollActivity(Guid userGuid, Guid activityGuid)
        {
            var user = await _databaseContext.Users.FirstAsync(x => x.Guid == userGuid);

            if(user is null)
            {
                return new ActivityResponseDto
                {
                    StatusCode = 400,
                    Message = $"Unable to find user with guid:{userGuid}",
                    Activities = null,
                    NewActivity = null,
                };
            }
            var currentActivity = await _databaseContext.Activities.Include(x => x.Attendants).FirstAsync(x => x.Guid == activityGuid);

            if(currentActivity is null)
            {
                return new ActivityResponseDto
                {
                    StatusCode = 400,
                    Message = $"Unable to find user with guid:{activityGuid}",
                    Activities = null,
                    NewActivity = null,
                };
            }

            var attendances = currentActivity.Attendants.Select(x => x.UserId);

            if (attendances.Contains(userGuid))
            {
                return new ActivityResponseDto
                {
                    StatusCode = 200,
                    Message = "User is already enrolled",
                    Activities = null,
                    NewActivity = ActivityDto.MapToActivityDto(currentActivity),
                };
            }

            var newAttendance = new Attendance { ActivityId = activityGuid, Activity = currentActivity, User = user, UserId = userGuid };
            currentActivity.Attendants.Add(newAttendance);
            await _databaseContext.SaveChangesAsync();

            var refreshedActivities = _databaseContext.Activities.Include(x => x.Attendants).Select(x => ActivityDto.MapToActivityDto(x)).ToList();

            return new ActivityResponseDto { StatusCode = 200, Activities = refreshedActivities, NewActivity = ActivityDto.MapToActivityDto(currentActivity), Message = "Enrolled" };
        }

        public async Task<ActivityResponseDto> GetActivities()
        {
            try
            { 
                var attendance = await _databaseContext.Attendances.ToListAsync();
                var activities =  _databaseContext.Activities.Include(x => x.Attendants).Select(x => ActivityDto.MapToActivityDto(x)).ToList();

                return new ActivityResponseDto
                {
                    StatusCode = 200,
                    Message = $"Found {activities.Count} activities",
                    Activities = activities,
                    NewActivity = null,
                };
            }
            catch(Exception)
            {
                return new ActivityResponseDto
                {
                    StatusCode = 500,
                    Message = "Something went wrong on the server",
                    Activities = null,
                    NewActivity = null,
                };
            }
        }

        public async Task<ActivityResponseDto> GetUserActivities(Guid userGuid)
        {
            var user = await _databaseContext.Users.FirstAsync(x => x.Guid == userGuid);
            if(user is null)
            {
                return new ActivityResponseDto
                {
                    StatusCode = 400,
                    Message = "User was not found in system",
                    Activities = null,
                    NewActivity = null
                };
            }
            var attendance = await _databaseContext.Attendances.Where(x => x.UserId == userGuid).Select(x => x.ActivityId).ToListAsync();
            if (!attendance.Any())
            {
                return new ActivityResponseDto
                {
                    StatusCode = 200,
                    Message = $"{user.Username} has not been assigned any activities.",
                    Activities = null,
                    NewActivity = null,
                };
            }

            var userActivities = await _databaseContext.Activities.Where(a => attendance.Contains(a.Guid)).Include(x => x.Attendants).Select(x => ActivityDto.MapToActivityDto(x)).ToListAsync();
            return new ActivityResponseDto { StatusCode = 200, Message = $"Found {userActivities.Count} activities" , Activities = userActivities, NewActivity =null};
        }

        private List<Guid> GetAttendancesGuid(ICollection<Attendance> attendances)
        {
            var users = _databaseContext.Users;
            var sb = new StringBuilder();
            List<Guid> attendantsGuids = new List<Guid>();
            foreach(var attendant in attendances)
            {
                if(users.Any(x => x.Guid == attendant.UserId))
                {
                    Console.WriteLine("Hello world ");
                    
                }

                return new List<Guid>();
            }

            return attendantsGuids;
        }
    }
}
