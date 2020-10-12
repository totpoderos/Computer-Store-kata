using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStore.Controllers;
using ComputerStore.Controllers.Request;
using ComputerStore.Controllers.Response;
using NUnit.Framework;

namespace ComputerStoreTests.Tests
{
    [TestFixture]
    public class OrdersControllerTests
    {
        [Test]
        public void Create_new_order_with_one_computer()
        {
            ComputersController computersController = new ComputersController();
            ComputerInfoDto computerInfoDto = computersController.GetComputerInfo("MacBook Pro 15");
            OrdersController ordersController = new OrdersController();
            
            List<OrderInformationDto> orders = ordersController.All();
            int previousNumberOfItems = orders.Count;

            NewOrderDto newOrderDto = new NewOrderDto
            {
                Computers = new List<string> {computerInfoDto.Id}
            };
            
            NewOrderIdDto newOrderIdDto = ordersController.CreateNewOrder(newOrderDto);
            Assert.IsNotNull(newOrderIdDto);
            
            orders = ordersController.All();
            Assert.AreEqual(previousNumberOfItems + 1, orders.Count);
            Assert.AreEqual(computerInfoDto.Id, orders.First().ComputersGuid.First());
        }
        
        [Test]
        public void Add_computer_to_existing_order()
        {
            ComputersController computersController = new ComputersController();
            ComputerInfoDto computerInfoDto = computersController.GetComputerInfo("MacBook Pro 15");
            OrdersController ordersController = new OrdersController();
            NewOrderDto newOrderDto = new NewOrderDto
            {
                Computers = new List<string> {computerInfoDto.Id}
            };
            NewOrderIdDto newOrderIdDto = ordersController.CreateNewOrder(newOrderDto);
            Assert.IsNotNull(newOrderIdDto);

            ComputerInfoDto secondComputerInfoDto = computersController.GetComputerInfo("iMac 27");
            ordersController.AddComputerToOrder(newOrderIdDto.Id, new AddComputersToOrderDto
            {
                ComputerId = new List<string> { secondComputerInfoDto.Id }
            });
            
            OrderInformationDto orderInformationDto = ordersController.GetOrderInformation(newOrderIdDto.Id);
            Assert.AreEqual(2, orderInformationDto.ComputersGuid.Count);
            Assert.AreEqual(computerInfoDto.Id, orderInformationDto.ComputersGuid[0]);
            Assert.AreEqual(secondComputerInfoDto.Id, orderInformationDto.ComputersGuid[1]);
        }
    }
}