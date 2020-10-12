using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStore.Controllers;
using ComputerStore.Controllers.Response;
using NUnit.Framework;

namespace ComputerStoreTests
{
    [TestFixture]
    public class ComputersControllerTests
    {
        [Test]
        public void Get_a_list_of_all_computers()
        {
            ComputersController computersController = new ComputersController();
            string expectedComputerName = "MacBook Pro 15";
            
            List<ComputerInfoDto> computers = computersController.All();
            
            Assert.AreEqual(5, computers.Count);
            Assert.AreEqual(expectedComputerName, computers.First().Name);
        }

        [Test]
        public void Get_computer_detailed_information_by_guid()
        {
            ComputersController computersController = new ComputersController();
            string computerName = "MacBook Pro 15";

            ComputerInfoDto computer = computersController.GetComputerInfo(computerName);
            
            Assert.AreEqual(computerName, computer.Name);
            Assert.AreEqual("Macbook pro 15' Description", computer.Description);
            Assert.AreEqual(1500, computer.Price);
        }
        
        [Test]
        public void Raise_an_error_when_searching_a_non_existing_computer()
        {
            ComputersController computersController = new ComputersController();
            string computerName = "Unknown";

            Exception exception = Assert.Throws<Exception>( () => computersController.GetComputerInfo(computerName));
            Assert.AreEqual("Computer not found, name: Unknown", exception.Message);
        }
        
        [Test]
        public void Get_computer_image()
        {
            ComputersController computersController = new ComputersController();
            string computerName = "iMac 27";
            string expectedSecondLineImageContet = "        " + " *    * " + "        " + "        " + "        " +
                                                   " ****** " + " ********";

            ComputerInfoDto computerInfo = computersController.GetComputerInfo(computerName);

            ComputerImageInfoDto imageInfo = computersController.GetComputerImage(computerInfo.Id);

            Assert.AreEqual(expectedSecondLineImageContet, imageInfo.Content[1]);
        }
        
        [Test]
        public void Raise_an_error_when_searching_image_info_of_non_existing_computer()
        {
            ComputersController computersController = new ComputersController();
            Exception exception = Assert.Throws<Exception>(() => computersController.GetComputerImage("unknown_guid"));
            Assert.AreEqual("Computer not found, guid: unknown_guid", exception.Message);
        }
    }
}