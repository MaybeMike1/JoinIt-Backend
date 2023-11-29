using JoinIt_Backend.Models.Dtos;
using JoinIt_Backend.Services;
using Moq;

namespace JoinIt_Test
{
    public class Tests
    {
        private Mock<IAuthProvider> _authProviderMock;

        [SetUp]
        public void Setup()
        {
            _authProviderMock = new Mock<IAuthProvider>();
        }

        [Theory]
        public void Test1()
        {
            var data = new AuthenticationRequestDto
            {
                Email ="string",
                Password = "string"
            };

            _authProviderMock.Setup(x => x.Login(data)).Returns(Task.FromResult(new AuthenticationResponseDto
            {
                StatusCode = 200,
                Email = "string",
                Guid = Guid.Empty,
                Message = ""
            }));
        }
    }
}