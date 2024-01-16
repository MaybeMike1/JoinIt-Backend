using JoinIt_Backend.Features.Authentication.Controller;
using JoinIt_Backend.Features.Authentication.Models.Dtos;
using JoinIt_Backend.Features.Authentication.Services;
using JoinIt_Backend.Shared.Data;
using JoinIt_Backend.Shared.Models;
using JoinIt_Test.Helpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace JoinIt_Test
{
    public class AuthenticationTests
    {
        private Mock<IAuthProvider> _authProviderMock;
        private Mock<DatabaseContext> _dbContextMock;
        private Mock<ICryptService> _cryptServiceMock;

        [SetUp]
        public void Setup()
        {
            _authProviderMock = new Mock<IAuthProvider>();
            _dbContextMock = new Mock<DatabaseContext>();
            _cryptServiceMock = new Mock<ICryptService>();
        }


        [Test]
        [TestCase("JohnnyCash@gmail.com", "CashM0ney!")]
        [TestCase("JohnDoe@gmail.com", "J0hnDoe")]
        public async Task Assert_BadRequest_On_Incorrect_Credentials(string email, string plainPassword)
        {
            var data = new AuthenticationRequestDto { Email = email, Password = plainPassword };
            int expectedStatusCode = 400;
            IAuthProvider authProvider = new AuthProvider(_dbContextMock.Object, _cryptServiceMock.Object);
            var fakeUserList = TestHelper.GetFakeUserList(10);

            _dbContextMock.Setup(x => x.Users)
                .ReturnsDbSet(fakeUserList);

            var result = await authProvider.Login(data);

            Assert.That(expectedStatusCode, Is.EqualTo(result.StatusCode));
        }

        [Test]
        [TestCase("test@gmail.com", "test123")]
        [TestCase("TEST@GMAIL.COM", "test123")]
        public async Task Assert_OK_On_Correct_Credentials(string email, string plainPassword)
        {
            var data = new AuthenticationRequestDto
            {
                Email = email,
                Password = plainPassword
            };
            var fakeUserList = TestHelper.GetFakeUserList(10);
            var expected = new AuthenticationResponseDto { Email = "test@gmail.com", Message = "User was sucessfully logged in.", StatusCode = 200 };

            // Arrange
            _dbContextMock.Setup(x => x.Users)
                .ReturnsDbSet(fakeUserList);

            // Act
            IAuthProvider authProvider = new AuthProvider(_dbContextMock.Object, _cryptServiceMock.Object);
            var result = await authProvider.Login(data);

            //Assert
            Assert.That(BCrypt.Net.BCrypt.Verify(data.Password, fakeUserList.First().PasswordHash), Is.True);

        }
        [Test]
        [TestCase("thomas@gmail.com", "th0mas001!", "thomastheman", "thomas", "jensen", "12345678")]
        [TestCase("peter@gmail.com", "petér001!", "petertheman", "peter", "petersen", "222222222")]
        public async Task Assert_OK_On_Register(string email, string plainPassword, string userName, string firstName, string lastName, string phoneNumber)
        {
            //Act
            var registerDto = new RegisterUserDto
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Phonenumber = phoneNumber,
                PlainPassword = plainPassword,
                Username = userName
            };

            int expectedStatusCode = 201;


            //Arrange 
            IAuthProvider authProvider = new AuthProvider(_dbContextMock.Object, _cryptServiceMock.Object);
            var fakeUserList = TestHelper.GetFakeUserList(10);
            _dbContextMock.Setup(x => x.Users)
                .ReturnsDbSet(fakeUserList);
            var result = await authProvider.Register(registerDto);


            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
        }

        [Test]
        [TestCase("test@gmail.com", "th0mas001!", "thomastheman", "thomas", "jensen", "12345678")]
        public async Task Assert_BadRequest_On_Register_If_User_Exists(string email, string plainPassword, string userName, string firstName, string lastName, string phoneNumber)
        {
            //Act
            var registerDto = new RegisterUserDto
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Phonenumber = phoneNumber,
                PlainPassword = plainPassword,
                Username = userName
            };

            int expectedStatusCode = 400;


            //Arrange 
            IAuthProvider authProvider = new AuthProvider(_dbContextMock.Object, _cryptServiceMock.Object);
            var fakeUserList = TestHelper.GetFakeUserList(10);
            _dbContextMock.Setup(x => x.Users)
                .ReturnsDbSet(fakeUserList);
            var result = await authProvider.Register(registerDto);

            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
        }
    }
}
