using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStore;
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
    }
}