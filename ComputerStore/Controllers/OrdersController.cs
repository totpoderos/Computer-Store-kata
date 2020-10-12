using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStore.Controllers.Request;
using ComputerStore.Controllers.Response;
using ComputerStore.Database;
using ComputerStore.Domain;

namespace ComputerStore.Controllers
{
    public class OrdersController
    {
        public NewOrderIdDto CreateNewOrder(NewOrderDto newOrderDto)
        {
            Order order = new Order()
            {
                Guid = Guid.NewGuid().ToString(),
                Date = DateTime.Now,
                Computers =  newOrderDto.Computers.Select(QueryService.FindComputerByGuid).ToList()
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
            Order order = QueryService.FindOrder(orderId);
            Entities entities = DatabaseContext.GetEntities();
            if (order == null)
            {
                order = new Order { Guid = orderId};
                order.Computers = new List<Computer>();
                entities.Orders.Add(order);
            }

            foreach (var computerId in computersToOrderDto.ComputerId)
            {
                Computer computer = QueryService.FindComputerByGuid(computerId);
                if (computer == null) throw new Exception("Cannot add computer to Order, Computer not found Id: " + computerId);
                order.Computers.Add(computer);
            }
            entities.Save();
        }

        public List<OrderInformationDto> All()
        {
            List<Order> orders = QueryService.GetAllOrders();
            return orders.Select(TransformationService.OrderToOrderInfromationDto).ToList();
        }

        public OrderInformationDto GetOrderInformation(string orderId)
        {
            Order order = QueryService.FindOrder(orderId);
            if (order == null) throw new Exception($"Order not found. Id: {orderId}");
            return TransformationService.OrderToOrderInfromationDto(order);
        }
    }

    public class NewOrderDto
    {
        public List<string> Computers { get; set; }
    }

    public class NewOrderIdDto
    {
        public string Id { get; set; }
    }
}