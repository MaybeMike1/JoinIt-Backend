using JoinIt_Backend.Shared.Data;
using JoinIt_Backend.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace JoinIt_Backend.Shared
{
    internal class Seeder
    {
        private readonly DatabaseContext _databaseContext;

        public Seeder(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void SeedActivityTypes()
        {
            _databaseContext.ActivityTypes.ExecuteDelete();
            var activityTypes = new List<ActivityType>
            {
                new ActivityType
                {
                    Description = "Fodbold - spark til en fodbold",
                    Type = "Fodbold",
                },
                new ActivityType
                {
                    Description = "Fodbold - spark til en fodbold",
                    Type = "Basket Ball",
                },
                new ActivityType
                {
                    Description = "Bouldering - kom det går kun opad",
                    Type = "Klatring",
                },
                new ActivityType
                {
                    Description = "Badminton - God med en ketjser?",
                    Type = "Badminton",
                },
                new ActivityType
                {
                    Description = "Fodbold - spark til en fodbold",
                    Type = "Fodbold",
                }
            };

            _databaseContext.ActivityTypes.AddRange(activityTypes);
            _databaseContext.SaveChanges();
        }

        public void SeedCountries()
        {
            _databaseContext.Countries.ExecuteDelete();
            for (int i = 0; i < 5; i++)
            {
                _databaseContext.Countries.Add(new Country { Id = Guid.NewGuid(), Name = Faker.Country.Name() });
                _databaseContext.SaveChanges();
            }
        }

        public void SeedZipCodes(List<Country> countries)
        {
            _databaseContext.Zips.ExecuteDelete();
            for (int i = 0; i < 5; i++)
            {
                _databaseContext.Zips.Add(new Zip { Guid = Guid.NewGuid(), PostalCode = Faker.Address.ZipCode(), Country = countries[i] });
                _databaseContext.SaveChanges();
            }
        }

        public void SeedAddresses(List<Zip> zipCodes)
        {
            _databaseContext.Addresses.ExecuteDelete();
            for (int i = 0; i < 5; i++)
            {
                _databaseContext.Addresses.Add(new Address { Zip = zipCodes[i], Floor = Faker.RandomNumber.Next(1, 5), StreetName = Faker.Address.StreetName(), StreetNumber = Faker.RandomNumber.Next(1, 100), Guid = Guid.NewGuid() });
                _databaseContext.SaveChanges();
            }
        }

        public List<Guid> SeedActivities(List<Address> addresses, List<Guid> userGuids)
        {
            var listOfActivityGuids = new List<Guid>
            {
                new Guid("cc0e40d3-21b4-4fa1-9104-0b09b05fe131"),
                new Guid("26b42c7e-a70e-409f-86c9-b85e7e1140b4"),
                new Guid("7e2a3e6e-6ee6-4921-ac56-8df4c65798cf"),
                new Guid("5f85472f-ba4b-40c5-9a73-a19d96dd78b6"),
                new Guid("d08b008f-e6d4-443d-be74-aedbfa443b27"),
            };

            _databaseContext.Activities.ExecuteDelete();

            for(int i = 0; i < addresses.Count; i++)
            {
                var user = _databaseContext.Users.Find(userGuids[i]);
                var activity = new Activity { Guid = Guid.NewGuid(), Address = addresses[i], Description = "Test 1", ActivityCosts = (decimal)100.00, Name = "Test Activity", VenueName = "Nørrebro Parken", Date = DateTime.Now, ActivityCreatorGuid = userGuids[i], IsCancelled = false, ActivityCapacity = 10 };
                _databaseContext.Activities.Add(activity);
                _databaseContext.Attendances.Add(new Attendance {Activity = activity, User = user, ActivityId = activity.Guid, UserId = user.Guid });
                _databaseContext.SaveChanges();

            }

            return listOfActivityGuids;
        }

        public List<Guid> SeedUsers()
        {
            _databaseContext.Users.ExecuteDelete();
            var listOfUserGuids = new List<Guid>
            {
                new Guid("99999999-9999-9999-9999-999999999999"),
                new Guid("ea22c054-35d9-48c2-85bd-8a05907593f3"),
                new Guid("7e5e39db-f931-4ffb-8bfe-27b27c7231d4"),
                new Guid("13a4a030-696b-47c4-8f91-7f973916c7a8"),
                new Guid("08b50df3-a738-4e24-b098-f20f5cd31f66"),
            };



            foreach (Guid guid in listOfUserGuids)
            {
                _databaseContext.Users.Add(new User
                {
                    Username = Faker.Name.First(),
                    Email = "Test@gmail.com",
                    LastName = Faker.Name.Last(),
                    Guid = guid,
                    PhoneNumber = Faker.Phone.Number(),
                    FirstName = Faker.Name.First(),
                    PostalCode = 2200,
                    SignUpDate = DateTime.Now,
                });
                _databaseContext.SaveChanges();
            }

            return listOfUserGuids;
        }

        public void SeedAttendances(List<Guid> userGuids, List<Guid> activityGuids)
        {
            _databaseContext.Attendances.ExecuteDelete();
            for(int i = 0; i < userGuids.Count; i++)
            {
                _databaseContext.Attendances.Add(new Attendance
                {
                    ActivityId = activityGuids[i],
                    UserId = userGuids[i]
                });
                _databaseContext.SaveChanges();
            }
        }
    }
}
