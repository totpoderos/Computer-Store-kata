using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStore.Controllers.Request;
using ComputerStore.Controllers.Response;
using ComputerStore.Database;
using ComputerStore.Domain;
using ComputerStore.Services;

namespace ComputerStore.Controllers
{
    public class OrdersController
    {
        public NewOrderIdDto CreateNewOrder(NewOrderDto newOrderDto)
        {
            ValidationService.ValidateNewOrderDto(newOrderDto);
            Order order = new Order()
            {
                Guid = Guid.NewGuid().ToString(),
                Date = DateTime.Now,
                OrderLines = newOrderDto.OrderLines.Select(TransformationService.NewOrderLineDtoToOrderLine).ToList(),
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
            ValidationService.ValidateAddComputersToOrderDto(computersToOrderDto);
            Order order = QueryService.FindOrder(orderId);
            Entities entities = DatabaseContext.GetEntities();
            if (order == null)
            {
                order = new Order { Guid = orderId};
                order.OrderLines =  new List<OrderLine>();
                entities.Orders.Add(order);
            }
            
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
            List<Order> orders = QueryService.GetAllOrders();
            return orders.Select(TransformationService.OrderToOrderInformationDto).ToList();
        }

        public OrderInformationDto GetOrderInformation(string orderId)
        {
            Order order = QueryService.FindOrder(orderId);
            if (order == null) throw new Exception($"Order not found. Id: {orderId}");
            return TransformationService.OrderToOrderInformationDto(order);
        }
    }
}