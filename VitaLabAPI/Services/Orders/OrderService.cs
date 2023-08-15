using VitaLabAPI.Exceptions.Orders;
using VitaLabAPI.Exceptions.Products;
using VitaLabAPI.Exceptions.Users;
using VitaLabAPI.Repositories.OrderDatas;
using VitaLabAPI.Repositories.Orders;
using VitaLabAPI.Repositories.Products;
using VitaLabAPI.Repositories.Users;
using VitaLabData.DTOs;
using VitLabData;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabAPI.Services.Orders
{
    /// <inheritdoc cref="IOrderService"/>
    public class OrderService : IOrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDataRepository _orderDataRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IProductRepository productRepository, IOrderRepository orderRepository, IOrderDataRepository orderDataRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDataRepository = orderDataRepository;
            _userRepository = userRepository;
        }

        /// <inheritdoc/>
        public async Task Create(OrderCreateRequest item)
        {
            var foundUser = await _userRepository.GetById(item.UserId);
            if(foundUser is null) 
            {
                throw new UserNotFoundException();
            }

            decimal totalPrice = 0;
            foreach(var orderDataCreateRequest in item.orderDataCreateRequests)
            {
                var product = await _productRepository.GetById(orderDataCreateRequest.ProducId);
                totalPrice += product.Price * orderDataCreateRequest.ProductQuantity;
            }
            
            var order = new Order(0, foundUser, DateTime.Now, totalPrice);
            await _orderRepository.Create(order);
        }

        /// <inheritdoc/>
        public async Task<int> CreateNewOrder(OrderCreateRequest orderCreateRequest)
        {
            var foundUser = await _userRepository.GetById(orderCreateRequest.UserId);
            if (foundUser is null)
            {
                throw new UserNotFoundException();
            }
            decimal totalPrice = 0;
            foreach (var orderDataCreateRequest in orderCreateRequest.orderDataCreateRequests)
            {
                var product = await _productRepository.GetById(orderDataCreateRequest.ProducId);
                totalPrice += product.Price * orderDataCreateRequest.ProductQuantity;
            }
            var orderToCreate = new Order(0, foundUser, DateTime.Now, totalPrice);
            var createdOrderId = await _orderRepository.CreateNewOrder(orderToCreate);
            return createdOrderId;
        }

        public async Task<IEnumerable<Order>> GetByPage(int page)
        {
            if (page <= 0)
                throw new ArgumentException("Page canno't be zero or less");
            return await _orderRepository.GetWithPagination(page);
        }

        /// <inheritdoc/>
        public async Task<Order> GetById(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order is null)
                throw new OrderNotFoundException();
            return order;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            var foundUser = await _userRepository.GetById(userId);
            if (foundUser is null)
                throw new UserNotFoundException();
            var foundOrders = await _orderRepository.GetOrdersByUserId(userId);
            return foundOrders;
        }

        /// <inheritdoc/>
        public async Task Edit(OrderDTO order)
        {
            try
            {
                var foundOrder = await GetById(order.Id);
                var foundUser = await _userRepository.GetById(order.Id);
                foundOrder.User = foundUser;
                foundOrder.Date = order.Date;
                foundOrder.TotalPrice = order.TotalPrice;
                await _orderRepository.Update(foundOrder);
            }
            catch (OrderNotFoundException)
            {
                throw;
            }
            catch (UserNotFoundException)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task DeleteById(int id)
        {
            try
            {
                var product = await GetById(id);
                await _orderRepository.DeleteById(id);
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
        }
    }
}
