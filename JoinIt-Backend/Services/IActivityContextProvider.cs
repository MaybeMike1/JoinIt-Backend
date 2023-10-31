using JoinIt_Backend.Data;
using JoinIt_Backend.Models;
using JoinIt_Backend.Models.Dtos.ActivityDtos;
using Microsoft.EntityFrameworkCore;

namespace JoinIt_Backend.Services
{
    public interface IActivityContextProvider
    {
        Task<ActivityResponseDto> GetActivities();

        Task<ActivityResponseDto> GetNearbyActivities(string userPostalCode);

        Task<ActivityResponseDto> CreateActivity(CreateActivityDto createActivityDto, Guid userGuid);

        Task<ActivityTypeResponseDto> GetActivityTypes();
    }

    public class ActivityContextProvider : IActivityContextProvider
    {
        private readonly DatabaseContext _databaseContext;

        public ActivityContextProvider(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<ActivityResponseDto> CreateActivity(CreateActivityDto createActivityDto, Guid userGuid)
        {

            try
            {
                var activity = new Activity();
                var requestUser = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Guid == userGuid);
                if (requestUser == null)
                {
                    return new ActivityResponseDto
                    {
                        StatusCode = 401,
                        Message = $"Requested user wasn't found by {nameof(userGuid)}. Couldn't process request.",
                        Activities = null,
                        NewActivity = null,
                    };
                }
                activity.Attendants.Add(requestUser);
                // Get ActivityType
                var currentActivityType = await _databaseContext.ActivityTypes.FirstOrDefaultAsync(x => x.Id == createActivityDto.ActivityTypeId);

                if (currentActivityType is null && createActivityDto.ActivityType != null)
                {
                    var activityType = new ActivityType
                    {
                        Type = createActivityDto.ActivityType.Type,
                        Description = createActivityDto.ActivityType.Description,
                    };
                    activity.ActivityType = activityType;
                }
                if (currentActivityType is not null)
                {
                    activity.ActivityType = currentActivityType;
                }

                // Get Address if exists else create Address
                var currentAddress = await _databaseContext.Addresses.FirstOrDefaultAsync(x => x == createActivityDto.Address);

                if (currentAddress == null)
                {
                    await _databaseContext.Addresses.AddAsync(createActivityDto.Address);
                    activity.Address = createActivityDto.Address;
                }

                if(currentAddress is not null)
                {
                    activity.Address = currentAddress;
                }

                //activity.Attendants.Add(requestUser);

                _databaseContext.Activities.Add(activity);

                return new ActivityResponseDto
                {
                    Activities = null,
                    StatusCode = 200,
                    Message = "Your activity is succesfully added to the system.",
                    NewActivity = activity,
                };
            }
            catch (Exception e)
            {
                return new ActivityResponseDto
                {
                    NewActivity = null,
                    Activities = null,
                    Message = e.Message,
                    StatusCode = 500,
                };
            }
            finally
            {
                await _databaseContext.DisposeAsync();
            }
        }

        public async Task<ActivityResponseDto> GetActivities()
        {
            try
            {
                var activitiesCount = await _databaseContext.Activities.CountAsync();

                if (activitiesCount != 0)
                {
                    return new ActivityResponseDto
                    {
                        Activities = _databaseContext.Activities.ToList(),
                        Message = "Successfully retrieved activities from datasource.",
                        StatusCode = 200
                    };
                }

                return new ActivityResponseDto
                {
                    Activities = _databaseContext.Activities.ToList(),
                    Message = "Activities are currently empty.. If this persists - contact admin.",
                    StatusCode = 200
                };

            }
            catch (Exception)
            {
                return new ActivityResponseDto
                {
                    Message = "Something went wrong on the server",
                    StatusCode = 500,
                    Activities = new List<Activity>(),
                };
            }
            finally
            {
                await _databaseContext.DisposeAsync();
            }
        }

        public async Task<ActivityTypeResponseDto> GetActivityTypes()
        {
            try
            {
                var result = _databaseContext.ActivityTypes.ToList();
                if (result.Any())
                {
                    return new ActivityTypeResponseDto
                    {
                        ActivityTypes = result,
                        StatusCode = 200,
                        Message = "Succesfully returned all activity types."
                    };
                }

                return new ActivityTypeResponseDto
                {
                    ActivityTypes = result,
                    StatusCode = 200,
                    Message = "Succesfully returned all activity types - however no activities found."
                };
            }
            catch (Exception e)
            {
                return new ActivityTypeResponseDto
                {
                    ActivityTypes = null,
                    StatusCode = 500,
                    Message = "Something went wrong on the server... Unable to get activity types.",
                };
            }
            finally
            {
                await _databaseContext.DisposeAsync();
            }
        }

        public async Task<ActivityResponseDto> GetNearbyActivities(string userPostalCode)
        {
            try
            {
                var potentialsActivitiesNearby = _databaseContext.Activities.Where(x => x.Address.Zip.PostalCode == userPostalCode).ToList();

                if (potentialsActivitiesNearby.Count == 0)
                {
                    return new ActivityResponseDto
                    {
                        Activities = potentialsActivitiesNearby,
                        Message = $"No activities in your current postalCode {userPostalCode} - please try again in a monenet.",
                        StatusCode = 200
                    };
                }

                return new ActivityResponseDto
                {
                    Activities = potentialsActivitiesNearby,
                    StatusCode = 200,
                    Message = $"Found {potentialsActivitiesNearby.Count} activities"
                };
            }
            catch (Exception)
            {
                return new ActivityResponseDto
                {
                    Activities = new List<Activity>(),
                    StatusCode = 500,
                    Message = "Something went wrong on the server."
                };
            }
            finally
            {
                await _databaseContext.DisposeAsync();
            }
        }
    }
}
