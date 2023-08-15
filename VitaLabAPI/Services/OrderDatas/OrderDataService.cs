using VitaLabAPI.Exceptions.OrderDatas;
using VitaLabAPI.Exceptions.Orders;
using VitaLabAPI.Exceptions.Products;
using VitaLabAPI.Repositories.OrderDatas;
using VitaLabAPI.Repositories.Orders;
using VitaLabAPI.Repositories.Products;
using VitLabData;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabAPI.Services.OrderDatas
{
    public class OrderDataService : IOrderDataService
    {
        private readonly IOrderDataRepository _orderDataRepository;
        private readonly IOrderRepository _orderRepository; 
        private readonly IProductRepository _productRepository;

        public OrderDataService(IOrderDataRepository orderDataRepository, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderDataRepository = orderDataRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task Create(OrderDataCreateRequest entity)
        {
            var foundOrder = await _orderRepository.GetById(entity.OrderId);
            if (foundOrder is null)
            {
                throw new OrderNotFoundException();
            }
            var foundProduct = await _productRepository.GetById(entity.ProducId);
            if (foundProduct is null)
            {
                throw new ProductNotFoundException();
            }
            var totalPrice = foundProduct.Price * entity.ProductQuantity;
            var order = new OrderData(0, foundOrder, foundProduct, entity.ProductQuantity, totalPrice);
            await _orderDataRepository.Create(order);
        }

        public Task CreateOrderData(int orderId, IEnumerable<OrderDataDTO> orderDatas)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderData> GetById(int id)
        {
            var foundOrderData = await _orderDataRepository.GetById(id);
            if (foundOrderData is null)
                throw new OrderDataNotFoundException();
            return foundOrderData;
        }

        public async Task<IEnumerable<OrderData>> GetOrderDatasByOrderId(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            if (order is null)
                throw new OrderNotFoundException();
            return await _orderDataRepository.GetOrderDatasByOrderId(orderId);
        }

        public async Task Edit(OrderDataDTO entity)
        {
            try
            {
                var orderData = await GetById(entity.Id);
                var foundOrder = await _orderRepository.GetById(entity.OrderId);
                if (foundOrder is null)
                {
                    throw new OrderNotFoundException();
                }
                var foundProduct = await _productRepository.GetById(entity.ProducId);
                if (foundProduct is null)
                {
                    throw new ProductNotFoundException();
                }
                orderData.Order = foundOrder;
                orderData.Product = foundProduct;
                orderData.ProductQuantity = entity.ProductQuantity;
                orderData.TotalPrice = foundProduct.Price * entity.ProductQuantity;
                await _orderDataRepository.Update(orderData);
            }
            catch (OrderDataNotFoundException)
            {
                throw;
            }
        }

        public async Task DeleteById(int id)
        {
            try
            {
                var orderData = await GetById(id);
                await _orderDataRepository.DeleteById(id);
            }
            catch (OrderDataNotFoundException)
            {
                throw;
            }
        }

        public Task<IEnumerable<OrderData>> GetByPage(int page)
        {
            throw new NotImplementedException();
        }


    }
}
