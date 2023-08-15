using Moq;
using VitaLabAPI.Repositories.Users;
using VitaLabAPI.Services.Users;
using VitLabData.DTOs.Create;

namespace VitaLabAPI.UnitTests.Services.Users
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private UserService _sut;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _sut = new UserService(_mockUserRepository.Object);
        }
        
        /// <summary>
        /// Valid data passed, should create new user.
        /// </summary>
        /// <returns></returnsS>
        [Test]
        public async Task Create_ShoulCreateUser()
        {
            var userCreateDto = new UserCreateRequest(
                "Pavel", 
                "Lucky", 
                "1234");
            _mockUserRepository
                .Setup(x => x
                .GetByLogin(It.IsAny<string>()))
                .Returns(() => Task.FromResult<User>(null));
            await _sut.Create(userCreateDto);
        }

        /// <summary>
        /// Invalid data passed, this login is already taken, should throw
        /// ArgumentException.
        /// </summary>
        [Test]
        public void Create_ShouldThrowArgumentException()
        {
            var userName = "Pavel";
            var userLogin = "Lucky";
            var userPassword = "1234";

            var userCreateDto = new UserCreateRequest(
                userName,
                userLogin,
                userPassword);

            var user = new User(
                1, 
                userName,
                userLogin,
                userPassword,
                new List<UserRoleEnum>() { UserRoleEnum.User });

            _mockUserRepository
                .Setup(x => x
                .GetByLogin(userCreateDto.Login))
                .ReturnsAsync(user);
            Assert.ThrowsAsync<ArgumentException>( async () =>
            {
                await _sut.Create(userCreateDto);
            });
        }

        [Test]
        public async Task FindByName_ShouldReturnRightResult()
        {
            var userNameToFind = "Pavel";

            var userCollection = new List<User>()
            {
                new User(1, userNameToFind, "Lucky", "1234"),
                new User(1, "Pavel Evstigneev", "Lucky", "1234")
            };
            _mockUserRepository.Setup(x => x
            .FindByName(userNameToFind))
            .ReturnsAsync(userCollection);

            var result = await _sut.FindByName(userNameToFind);

            Assert.True(result.Any(x => x.Name == userNameToFind));
        }
    }
}
