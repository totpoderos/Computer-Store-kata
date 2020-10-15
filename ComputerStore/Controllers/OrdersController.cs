using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStore.Controllers.Request;
using ComputerStore.Controllers.Request.Order;
using ComputerStore.Controllers.Response;
using ComputerStore.Controllers.Response.Order;
using ComputerStore.Database;
using ComputerStore.Domain;
using ComputerStore.Services;

namespace ComputerStore.Controllers
{
    public class OrdersController
    {
        private readonly string _username;
        private readonly string _password;
        
        public OrdersController(string username, string password)
        {
            _username = username;
            _password = password;
        }
        public NewOrderIdDto CreateNewOrder(NewOrderDto newOrderDto)
        {
            User user = AuthenticationService.AuthenticateUser(_username, _password);
            ValidationService.ValidateNewOrderDto(newOrderDto);
            Order order = new Order()
            {
                Guid = Guid.NewGuid().ToString(),
                Date = DateTime.Now,
                OrderLines = newOrderDto.OrderLines.Select(TransformationService.NewOrderLineDtoToOrderLine).ToList(),
                User = user
            };
            
            Entities entities = DatabaseContext.GetEntities();
            entities.Orders.Add(order);
            entities.Save();
            return new NewOrderIdDto
            {
                Id = order.Guid
            };
        }
        public void AddComputerToOrder(string orderId, AddComputersToOrderDto computersToOrderDto)
        {
            User user = AuthenticationService.AuthenticateUser(_username, _password);
            ValidationService.ValidateAddComputersToOrderDto(computersToOrderDto);
            Order order = QueryService.FindOrder(orderId);
            if (!order.User.Username.Equals(user.Username))
                throw new Exception("Order doesn't belong to user. User: " + _username);
            Entities entities = DatabaseContext.GetEntities();

            foreach (var orderLineDto in computersToOrderDto.OrderLines)
            {
                OrderLine orderLine =
                    order.OrderLines.FirstOrDefault(ol => ol.Computer.Guid == orderLineDto.ComputerId);
                if (orderLine != null)
                    orderLine.Quantity += orderLineDto.Quantity;
                else
                    order.OrderLines.Add(TransformationService.AddOrderLineToOrderLine(orderLineDto));
            }
            entities.Save();
        }

        public List<OrderInformationDto> All()
        {
            User user = AuthenticationService.AuthenticateUser(_username, _password);
            List<Order> orders = QueryService.GetAllOrders(user);
            return orders.Select(TransformationService.OrderToOrderInformationDto).ToList();
        }

        public OrderInformationDto GetOrderInformation(string orderId)
        {
            User user = AuthenticationService.AuthenticateUser(_username, _password);
            Order order = QueryService.FindOrder(orderId);
            if (order == null) throw new Exception($"Order not found. Id: {orderId}");
            if (!order.User.Username.Equals(user.Username))
                throw new Exception("Order doesn't belong to user. User: " + _username);
            return TransformationService.OrderToOrderInformationDto(order);
        }
    }
}