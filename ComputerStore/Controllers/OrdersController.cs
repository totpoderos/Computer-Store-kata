using System;
using System.Collections.Generic;
using ComputerStore.Controllers.Request;
using ComputerStore.Database;

namespace ComputerStore.Controllers
{
    public class OrdersController
    {
        public void AddComputerToOrder(string orderId, AddComputerToOrderDto computerToOrderDto)
        {
            Order order = QueryService.FindOrder(orderId);
            Entities entities = DatabaseContext.GetEntities();
            if (order == null)
            {
                order = new Order { Guid = Guid.NewGuid().ToString()};
                order.Computers = new List<Computer>();
                entities.Orders.Add(order);
            }
            Computer computer = QueryService.FindComputerByGuid(computerToOrderDto.ComputerId);
            if (computer == null) throw new Exception("Cannot add computer to Order, Computer not found Id: " + computerToOrderDto.ComputerId);
            order.Computers.Add(computer);
            entities.Save();
        }
    }
}