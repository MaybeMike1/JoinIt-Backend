using JoinIt_Backend.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Test.Helpers
{
    public class TestHelper
    {

        public static List<Activity> GetFakeActivities(int count, List<User> users)
        {
            List<Activity> list = new List<Activity>();

            for(int i = 0; i < count; i++)
            {
                list.Add(new Activity
                {
                    Guid = Guid.NewGuid(),
                    ActivityCapacity = Faker.RandomNumber.Next(1, 10),
                    Date = DateTime.Now,
                    Name = Faker.Company.Name(),
                    Description = Faker.Lorem.Paragraph(4),
                    VenueName = Faker.Company.Name(),
                    ActivityCreatorGuid = users[i].Guid

                });
            }

            return list;
        }
        public static List<User> GetFakeUserList(int count)
        {
            List<User> users = new List<User>();

            for(int i = 0; i < count; ++i)
            {
                users.Add(new User
                {
                    Guid = Guid.NewGuid(),
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    PhoneNumber = Faker.Phone.Number(),
                    Email = "test@gmail.com",
                    Username = "John_doe",
                    PostalCode = Faker.RandomNumber.Next(4),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("test123")
                });
            }

            return users;
            
        }
    }
}
