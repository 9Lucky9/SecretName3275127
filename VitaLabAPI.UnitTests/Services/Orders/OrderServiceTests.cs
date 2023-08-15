using Moq;
using VitaLabAPI.Repositories.OrderDatas;
using VitaLabAPI.Repositories.Orders;
using VitaLabAPI.Repositories.Products;
using VitaLabAPI.Repositories.Users;
using VitaLabAPI.Services.Orders;

namespace VitaLabAPI.UnitTests.Services.Orders
{
    public class OrderServiceTests
    {
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IOrderRepository> _mockOrderRepository;
        private Mock<IOrderDataRepository> _mockOrderDataRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private OrderService _sut;

        [SetUp]
        public void SetUp()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockOrderDataRepository = new Mock<IOrderDataRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _sut = new OrderService(
                _mockProductRepository.Object,
                _mockOrderRepository.Object,
                _mockOrderDataRepository.Object,
                _mockUserRepository.Object);
        }
    }
}
