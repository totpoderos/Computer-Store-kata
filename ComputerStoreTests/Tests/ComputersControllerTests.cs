using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStore.Controllers;
using ComputerStore.Controllers.Response;
using ComputerStore.Controllers.Response.Computer;
using NUnit.Framework;

namespace ComputerStoreTests.Tests
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

            ComputerInfoDto computer = computersController.FindComputerByName(computerName);
            
            Assert.AreEqual(computerName, computer.Name);
            Assert.AreEqual("Macbook pro 15' Description", computer.Description);
            Assert.AreEqual(1500, computer.Price);
        }
        
        [Test]
        public void Raise_an_error_when_searching_a_non_existing_computer()
        {
            ComputersController computersController = new ComputersController();
            string computerName = "Unknown";

            Exception exception = Assert.Throws<Exception>( () => computersController.FindComputerByName(computerName));
            Assert.AreEqual("Computer not found, name: Unknown", exception.Message);

            exception = Assert.Throws<Exception>( () => computersController.GetComputerInfo("unknownid"));
            Assert.AreEqual("Computer not found, Id: unknownid", exception.Message);
        }
        
        [Test]
        public void Get_computer_image()
        {
            ComputersController computersController = new ComputersController();
            string computerName = "iMac 27";
            string [] expectedResult =
            {
                "                                                        ",
                "  *      *    *                           ****   ****** ",
                "         **  **   ***     ***            *    *       * ",
                " **      * ** *      *   *                    *      *  ",
                "  *      *    *   ****   *                ****      *   ",
                "  *      *    *  *   *   *               *         *    ",
                " ***     *    *   ****    ***            ******    *    ",
                "                                                        "
            };
            ComputerInfoDto computerInfo = computersController.FindComputerByName(computerName);

            ComputerImageInfoDto imageInfo = computersController.GetComputerImage(computerInfo.Id);
            
            imageInfo.Content.ForEach(Console.WriteLine);
            Assert.AreEqual(expectedResult, imageInfo.Content);
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