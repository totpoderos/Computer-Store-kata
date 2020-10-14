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
        //Create user
        //Update user
        //Delete user
    }
}