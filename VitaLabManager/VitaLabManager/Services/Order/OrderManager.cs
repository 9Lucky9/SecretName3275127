using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VitaLabManager.MVVM.Models;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabManager.Services.Order
{
    public class OrderManager : IOrderManager
    {
        private readonly IVitaLabApiWrapper _apiWrapper;
        private const string APIEndpoint = "Order";

        public OrderManager(IVitaLabApiWrapper apiWrapper)
        {
            _apiWrapper = apiWrapper;
        }

        /// <inheritdoc/>
        public async Task<int> CreateNewOrder(IEnumerable<OrderDataCreateRequest> orderDataCreateRequests)
        {
            var apiEndPoint = $"{APIEndpoint}/Post";
            var orderCreateRequest = new OrderCreateRequest()
            {
                UserId = _apiWrapper.CurrentUser.Id,
                orderDataCreateRequests = orderDataCreateRequests
            };
            var json = JsonContent.Create(orderCreateRequest);
            var response = await _apiWrapper.HttpClient.PostAsync(apiEndPoint, json);
            var createdOrderId = await response.Content.ReadAsStringAsync();
            return Convert.ToInt32(createdOrderId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<OrderDTO>> LoadByPage(int pageNumber)
        {
            var apiEndPoint = $"{APIEndpoint}/GetByPage";
            var request = $"{apiEndPoint}?page={pageNumber}";
            var response = await _apiWrapper.HttpClient.GetAsync(request);
            var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderDTO>>();
            return orders;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<OrderDTO>> GetByUserId(int userId)
        {
            var apiEndPoint = $"{APIEndpoint}/GetByUserId";
            var request = $"{apiEndPoint}?userId={userId}";
            var response = await _apiWrapper.HttpClient.GetAsync(request);
            var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderDTO>>();
            return orders;
        }

        /// <inheritdoc/>
        public Task UpdateOrder(IEnumerable<OrderDataCreateRequest> orderDataCreateRequests)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task DeleteOrder(int id)
        {
            var apiEndPoint = $"{APIEndpoint}/Delete";
            var request = $"{apiEndPoint}?orderId={id}";
            var response = await _apiWrapper.HttpClient.DeleteAsync(request);
        }

        public async Task<IEnumerable<OrderDataDTO>> GetOrderDataDTOsByOrderId(int orderId)
        {
            var apiEndPoint = $"OrderData/GetByOrderId";
            var request = $"{apiEndPoint}?orderId={orderId}";
            var response = await _apiWrapper.HttpClient.GetAsync(request);
            var orderDatas = await response.Content.ReadFromJsonAsync<IEnumerable<OrderDataDTO>>();
            return orderDatas;
        }

        public async Task<IEnumerable<MyOrder>> GetOrdersOfAuthorizedUser()
        {
            var ordersDTO = await GetByUserId(_apiWrapper.CurrentUser.Id);
            var myOrders = new List<MyOrder>();
            foreach(var order in ordersDTO)
            {
                var myOrder = new MyOrder()
                {
                    Id = order.Id,
                    Date = order.Date,
                    OrderDatas = await GetOrderDataDTOsByOrderId(order.Id),
                    TotalPrice = order.TotalPrice,
                };
                myOrders.Add(myOrder);
            }
            return myOrders;
        }

    }
}
