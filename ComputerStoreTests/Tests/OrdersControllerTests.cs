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
            ComputerInfoDto computerInfoDto = computersController.FindComputerByName("MacBook Pro 15");
            OrdersController ordersController = new OrdersController();
            int computerQuantity = 1;
            Decimal expectedPrice = computerInfoDto.Price;
            
            List<OrderInformationDto> orders = ordersController.All();
            int previousNumberOfItems = orders.Count;

            NewOrderDto newOrderDto = new NewOrderDto
            {
                OrderLines = new List<NewOrderLineDto> { new NewOrderLineDto { ComputerId = computerInfoDto.Id, Quantity = computerQuantity}}
            };
            
            NewOrderIdDto newOrderIdDto = ordersController.CreateNewOrder(newOrderDto);
            Assert.IsNotNull(newOrderIdDto);
            
            orders = ordersController.All();
            OrderInformationDto order = ordersController.All().First(ord => ord.OrderId == newOrderIdDto.Id);
            Assert.AreEqual(previousNumberOfItems + 1, orders.Count);
            List<OrderLineInfoDto> newOrderOrderLinesInfo = order.OrderLinesInfo;
            Assert.AreEqual(1, newOrderOrderLinesInfo.Count);
            Assert.AreEqual(computerInfoDto.Id, newOrderOrderLinesInfo.First().ComputerId);
            Assert.AreEqual(computerQuantity, newOrderOrderLinesInfo.First().Quantity);
            Assert.AreEqual(expectedPrice, order.Price);
        }
        
        [Test]
        public void Raise_an_error_when_creating_new_order_with_non_existing_computer()
        {
            OrdersController ordersController = new OrdersController();
            
            NewOrderDto newOrderDto = new NewOrderDto
            {
                OrderLines = new List<NewOrderLineDto> { new NewOrderLineDto { ComputerId = "Unknown", Quantity = 1}},
            };
            
            Exception exception = Assert.Throws<Exception>(() => ordersController.CreateNewOrder(newOrderDto));
            Assert.IsNotNull(exception);
            Assert.AreEqual("Computer not found. Id: Unknown", exception.Message);
        }
        
        [Test]
        public void Raise_an_error_when_creating_new_order_with_negative_quantity()
        {
            ComputersController computersController = new ComputersController();
            ComputerInfoDto computerInfoDto = computersController.FindComputerByName("MacBook Pro 15");
            OrdersController ordersController = new OrdersController();
            NewOrderDto newOrderDto = new NewOrderDto
            {
                OrderLines = new List<NewOrderLineDto> { new NewOrderLineDto { ComputerId = computerInfoDto.Id, Quantity = -1}}
            };
            
            Exception exception = Assert.Throws<Exception>(() => ordersController.CreateNewOrder(newOrderDto));
            
            Assert.IsNotNull(exception);
            Assert.AreEqual("Computer quantity cannot be negative", exception.Message);
        }
        
        [Test]
        public void Add_second_computer_to_existing_order()
        {
            ComputersController computersController = new ComputersController();
            ComputerInfoDto computerInfoDto = computersController.FindComputerByName("MacBook Pro 15");
            Decimal expectedPrice = computerInfoDto.Price;
            OrdersController ordersController = new OrdersController();
            NewOrderDto newOrderDto = new NewOrderDto
            {
                OrderLines = new List<NewOrderLineDto> { new NewOrderLineDto { ComputerId = computerInfoDto.Id, Quantity = 1}}
            };
            NewOrderIdDto newOrderIdDto = ordersController.CreateNewOrder(newOrderDto);
            Assert.IsNotNull(newOrderIdDto);

            ComputerInfoDto secondComputerInfoDto = computersController.FindComputerByName("iMac 27");
            expectedPrice += secondComputerInfoDto.Price * 2;
            ordersController.AddComputerToOrder(newOrderIdDto.Id, new AddComputersToOrderDto
            {
                OrderLines = new List<AddOrderLineDto> { new AddOrderLineDto { ComputerId = secondComputerInfoDto.Id, Quantity = 2}}
            });
            OrderInformationDto orderInformationDto = ordersController.GetOrderInformation(newOrderIdDto.Id);
            
            Assert.AreEqual(2, orderInformationDto.OrderLinesInfo.Count);
            Assert.AreEqual(computerInfoDto.Id, orderInformationDto.OrderLinesInfo[0].ComputerId);
            Assert.AreEqual(secondComputerInfoDto.Id, orderInformationDto.OrderLinesInfo[1].ComputerId);
            Assert.AreEqual(expectedPrice, orderInformationDto.Price);
        }
        
        [Test]
        public void Adding_existing_computer_to_existing_order_increases_OrderLine_quantity()
        {
            ComputersController computersController = new ComputersController();
            ComputerInfoDto computerInfoDto = computersController.FindComputerByName("MacBook Pro 15");
            Decimal expectedPrice = computerInfoDto.Price;
            OrdersController ordersController = new OrdersController();
            NewOrderDto newOrderDto = new NewOrderDto
            {
                OrderLines = new List<NewOrderLineDto> { new NewOrderLineDto { ComputerId = computerInfoDto.Id, Quantity = 1}},
            };
            NewOrderIdDto newOrderIdDto = ordersController.CreateNewOrder(newOrderDto);
            Assert.IsNotNull(newOrderIdDto);

            ComputerInfoDto secondComputerInfoDto = computersController.GetComputerInfo(computerInfoDto.Id);
            expectedPrice += secondComputerInfoDto.Price;
            ordersController.AddComputerToOrder(newOrderIdDto.Id, new AddComputersToOrderDto
            {
                OrderLines = new List<AddOrderLineDto> { new AddOrderLineDto { ComputerId = secondComputerInfoDto.Id, Quantity = 1}},
            });
            OrderInformationDto orderInformationDto = ordersController.GetOrderInformation(newOrderIdDto.Id);
            
            Assert.AreEqual(1, orderInformationDto.OrderLinesInfo.Count);
            Assert.AreEqual(computerInfoDto.Id, orderInformationDto.OrderLinesInfo[0].ComputerId);
            Assert.AreEqual(expectedPrice, orderInformationDto.Price);
        }
        
        [Test]
        public void Raise_an_error_when_adding_a_non_existing_computer_to_existing_order()
        {
            ComputersController computersController = new ComputersController();
            ComputerInfoDto computerInfoDto = computersController.FindComputerByName("MacBook Pro 15");
            OrdersController ordersController = new OrdersController();
            NewOrderDto newOrderDto = new NewOrderDto
            {
                OrderLines = new List<NewOrderLineDto> { new NewOrderLineDto { ComputerId = computerInfoDto.Id, Quantity = 1}},
            };
            NewOrderIdDto newOrderIdDto = ordersController.CreateNewOrder(newOrderDto);
            Assert.IsNotNull(newOrderIdDto);

            Exception exception = Assert.Throws<Exception>(() =>
            ordersController.AddComputerToOrder(newOrderIdDto.Id, new AddComputersToOrderDto
            {
                OrderLines = new List<AddOrderLineDto> { new AddOrderLineDto { ComputerId = "Unknown", Quantity = 1}},
            }));
            
            Assert.AreEqual("Cannot add computer to Order, Computer not found Id: Unknown", exception.Message);
       }
        
        [Test]
        public void Raise_an_error_when_adding_a_computer_to_existing_order_with_negative_quantity()
        {
            ComputersController computersController = new ComputersController();
            ComputerInfoDto computerInfoDto = computersController.FindComputerByName("MacBook Pro 15");
            OrdersController ordersController = new OrdersController();
            NewOrderDto newOrderDto = new NewOrderDto
            {
                OrderLines = new List<NewOrderLineDto> { new NewOrderLineDto { ComputerId = computerInfoDto.Id, Quantity = 1}},
            };
            NewOrderIdDto newOrderIdDto = ordersController.CreateNewOrder(newOrderDto);
            Assert.IsNotNull(newOrderIdDto);

            Exception exception = Assert.Throws<Exception>(() =>
            ordersController.AddComputerToOrder(newOrderIdDto.Id, new AddComputersToOrderDto
            {
                OrderLines = new List<AddOrderLineDto> { new AddOrderLineDto { ComputerId = computerInfoDto.Id, Quantity = -1}},
            }));
            
            Assert.AreEqual("Computer quantity cannot be negative", exception.Message);
       }
    }
}