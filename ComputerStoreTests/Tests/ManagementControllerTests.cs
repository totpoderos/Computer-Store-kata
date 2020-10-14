using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStore.Controllers;
using ComputerStore.Controllers.Request;
using ComputerStore.Controllers.Response;
using ComputerStore.Domain;
using NUnit.Framework;

namespace ComputerStoreTests.Tests
{
    [TestFixture]
    public class ManagementControllerTests
    {
        //Create computer
        [Test]
        public void Create_new_computer()
        {
            ComputersController computersController = new ComputersController();
            int numberOfComputers = computersController.All().Count;
            ManagementController managementController = new ManagementController();

            managementController.AddComputer(new AddComputerDto
            {
                Name = "TestComputer",
                Description = "New computer",
                Price = 1500,
                ImageFilename = "test.png"
            });

            var allComputers = computersController.All();
            int currentNumberOfComputers = allComputers.Count;
            Assert.AreEqual(numberOfComputers + 1, currentNumberOfComputers);
            Assert.IsNotNull(allComputers.First(comp => comp.Name == "TestComputer"));
        }

        [Test]
        public void Raise_an_error_when_creating_a_computer_with_empty_name()
        {
            ManagementController managementController = new ManagementController();

            Exception exception = Assert.Throws<Exception>(() =>
                managementController.AddComputer(new AddComputerDto
                {
                    Name = null,
                    Description = "New computer",
                    Price = 1500,
                    ImageFilename = "test.png"
                }));

            Assert.AreEqual("Computer Name cannot be null or empty", exception.Message);
        }

        [Test]
        public void Raise_an_error_when_creating_a_computer_with_empty_description()
        {
            ManagementController managementController = new ManagementController();

            Exception exception = Assert.Throws<Exception>(() =>
                managementController.AddComputer(new AddComputerDto
                {
                    Name = "TestComputer",
                    Description = string.Empty,
                    Price = 1500,
                    ImageFilename = "test.png"
                }));

            Assert.AreEqual("Computer Description cannot be null or empty", exception.Message);
        }

        [Test]
        public void Raise_an_error_when_creating_a_computer_with_empty_filename()
        {
            ManagementController managementController = new ManagementController();

            Exception exception = Assert.Throws<Exception>(() =>
                managementController.AddComputer(new AddComputerDto
                {
                    Name = "TestComputer",
                    Description = "New Computer",
                    Price = 1500,
                    ImageFilename = string.Empty
                }));

            Assert.AreEqual("Computer Filename cannot be null or empty", exception.Message);
        }

        [Test]
        public void Raise_an_error_when_creating_a_computer_with_negative_price()
        {
            ManagementController managementController = new ManagementController();

            Exception exception = Assert.Throws<Exception>(() =>
                managementController.AddComputer(new AddComputerDto
                {
                    Name = "TestComputer",
                    Description = "New Computer",
                    Price = -1500,
                    ImageFilename = "computer.png"
                }));

            Assert.AreEqual("Computer price cannot be negative", exception.Message);
        }

        //Update computer
        [Test]
        public void Update_computer_name()
        {
            ComputersController computersController = new ComputersController();
            ManagementController managementController = new ManagementController();

            managementController.AddComputer(new AddComputerDto
            {
                Name = "TestComputer",
                Description = "New computer",
                Price = 1500,
                ImageFilename = "test.png"
            });

            ComputerInfoDto computerInfoDto = computersController.FindComputerByName("TestComputer");
            UpdateComputerDto updateComputerDto = new UpdateComputerDto
            {
                Name = "Amstrad CPC",
                Description = "New Amstrad with 128Kb",
                Price = 1000,
                ImageFilename = "amstrad.png"
            };

            managementController.UpdateComputer(computerInfoDto.Id, updateComputerDto);

            ComputerInfoDto updatedComputer = computersController.GetComputerInfo(computerInfoDto.Id);
            Assert.AreEqual(computerInfoDto.Id, updatedComputer.Id);
            Assert.AreEqual(updateComputerDto.Name, updatedComputer.Name);
            Assert.AreEqual(updateComputerDto.Description, updatedComputer.Description);
            Assert.AreEqual(updateComputerDto.Price, updatedComputer.Price);
            Assert.AreEqual(updateComputerDto.ImageFilename, updatedComputer.ImageFilename);
        }

        //Delete computer
        [Test]
        public void Delete_computer()
        {
            ComputersController computersController = new ComputersController();
            ManagementController managementController = new ManagementController();
            managementController.AddComputer(new AddComputerDto
            {
                Name = "TestComputer",
                Description = "New computer",
                Price = 1500,
                ImageFilename = "test.png"
            });
            ComputerInfoDto computerInfoDto = computersController.FindComputerByName("TestComputer");

            managementController.DeleteComputer(computerInfoDto.Id);

            Exception exception = Assert.Throws<Exception>(() => computersController.GetComputerInfo("TestComputer"));
            Assert.AreEqual("Computer not found, Id: TestComputer", exception.Message);
        }

        //Delete Order
        [Test]
        public void Delete_existing_order()
        {
            ComputersController computersController = new ComputersController();
            string macBookId = computersController.FindComputerByName("MacBook Pro 15").Id;
            string imacId = computersController.FindComputerByName("iMac 27").Id;
            NewOrderDto newOrderDto = new NewOrderDto
            {
                OrderLines = new List<NewOrderLineDto>
                {
                    new NewOrderLineDto {ComputerId = macBookId, Quantity = 2},
                    new NewOrderLineDto {ComputerId = imacId, Quantity = 2}
                }
            };
            OrdersController ordersController = new OrdersController();
            List<OrderInformationDto> previousAllOrders = ordersController.All();
            NewOrderIdDto orderIdDto = ordersController.CreateNewOrder(newOrderDto);
            Assert.IsTrue(ordersController.All().Exists(order => order.OrderId.Equals(orderIdDto.Id)));
            ManagementController managementController = new ManagementController();

            managementController.DeleteOrder(orderIdDto.Id);

            List<OrderInformationDto> allOrders = ordersController.All();
            Assert.IsFalse(allOrders.Exists(order => order.OrderId.Equals(orderIdDto.Id)));
        }

        //Create user
        [Test]
        public void Create_new_user()
        {
            NewUserDto newUserDto = new NewUserDto
            {
                Name = "Robert",
                Surname = "Martin",
                Username = "RoberMartin",
                Password = "123456",
                Email = "unclebob@mail.com",
                IsRoot = false
            };
            ManagementController managementController = new ManagementController();

            NewUserIdDto newUserIdDto = managementController.CreateUser(newUserDto);

            Assert.IsNotNull(newUserIdDto);
            UserInfoDto userInfoDto = managementController.GetUser(newUserIdDto.Id);
            Assert.IsNotNull(userInfoDto);
            Assert.AreEqual(newUserDto.Name, userInfoDto.Name);
            Assert.AreEqual(newUserDto.Surname, userInfoDto.Surname);
            Assert.AreEqual(newUserDto.Username, userInfoDto.Username);
            Assert.AreEqual(newUserDto.Email, userInfoDto.Email);
            Assert.AreEqual(newUserDto.IsRoot, userInfoDto.IsRoot);
        }

        [Test]
        public void Raise_an_error_when_creating_a_user_with_existing_username()
        {
            NewUserDto newUserDto = new NewUserDto
            {
                Name = "Robert",
                Surname = "Martin",
                Username = "UncleBob",
                Password = "123456",
                Email = "unclebob@mail.com",
                IsRoot = false
            };
            NewUserDto duplicatedUserDto = new NewUserDto
            {
                Name = "John",
                Surname = "Smith",
                Username = "UncleBob",
                Password = "1234568",
                Email = "johnsmith@mail.com",
                IsRoot = false
            };
            ManagementController managementController = new ManagementController();
            NewUserIdDto newUserIdDto = managementController.CreateUser(newUserDto);

            Exception exception = Assert.Throws<Exception>(() => managementController.CreateUser(duplicatedUserDto));
            Assert.AreEqual("Cannot create user. Username is duplicate. Username: UncleBob", exception.Message);
        }

        //Update user
        [Test]
        public void Update_existing_user()
        {
            NewUserDto newUserDto = new NewUserDto
            {
                Name = "Martin",
                Surname = "Fowler",
                Username = "MartinFowler",
                Password = "123456",
                Email = "martinfowler@mail.com",
                IsRoot = false
            };

            UpdateUserDto updatedUserDto = new UpdateUserDto
            {
                Name = "John",
                Surname = "Smith",
                Username = "JohnSmith",
                Password = "12345682",
                Email = "johnsmith@mail.com",
                IsRoot = true
            };
            ManagementController managementController = new ManagementController();
            NewUserIdDto newUserIdDto = managementController.CreateUser(newUserDto);

            managementController.UpdateUser(newUserIdDto.Id, updatedUserDto);

            UserInfoDto userInfoDto = managementController.GetUser(newUserIdDto.Id);
            Assert.AreEqual(updatedUserDto.Name, userInfoDto.Name);
            Assert.AreEqual(updatedUserDto.Surname, userInfoDto.Surname);
            Assert.AreEqual(updatedUserDto.Email, userInfoDto.Email);
            Assert.AreEqual(updatedUserDto.Username, userInfoDto.Username);
            Assert.AreEqual(updatedUserDto.IsRoot, userInfoDto.IsRoot);
        }

        //Delete user
        [Test]
        public void Delete_existing_user()
        {
            NewUserDto newUserDto = new NewUserDto
            {
                Name = "Martin",
                Surname = "Fowler",
                Username = "MartinFowler_delete",
                Password = "123456",
                Email = "martinfowler@mail.com",
                IsRoot = false
            };
            ManagementController managementController = new ManagementController();
            NewUserIdDto newUserIdDto = managementController.CreateUser(newUserDto);

            managementController.DeleteUser(newUserIdDto.Id);

            Exception exception = Assert.Throws<Exception>(() => managementController.GetUser(newUserIdDto.Id));
            Assert.AreEqual("User not found. Id: " + newUserIdDto.Id, exception.Message);
        }
    }
}