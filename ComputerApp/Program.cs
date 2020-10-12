using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStore;
using ComputerStore.Controllers;
using ComputerStore.Controllers.Request;
using ComputerStore.Controllers.Response;
using ComputerStore.Database;

namespace ComputerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateAndRemoveAComputer();
            //FindAComputerByName("imac 27");
            // AddNewComputerUseCase();
            var computersController = new ComputersController();
            ComputerInfoDto computerInfoDto = computersController.GetComputerInfo("iMac 27");
            OrdersController ordersController = new OrdersController();
            ordersController.AddComputerToOrder(string.Empty, new AddComputersToOrderDto { ComputerId = computerInfoDto.Id});
            List<ComputerInfoDto> computers = computersController.All();
            computers.ToList().ForEach(PrintComputerInfo);
            Console.WriteLine("Orders");
            DatabaseContext.GetEntities().Orders.ForEach(order =>
            {
                Console.WriteLine("Order: " + order.Guid);
                order.Computers.ForEach(PrintComputerInfo);
            });
        }

        private static void AddNewComputerUseCase()
        {
            AddComputerDto newAddComputer = new AddComputerDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = "iMac 25",
                Price = -1350,
                ImageFilename = "iMac225.png"
            };
            ManagementController managementController = new ManagementController();
            managementController.AddComputer(newAddComputer);
        }

        private static void FindAComputerByName(string computerName)
        {
            var computer = QueryService.FindComputer(computerName);
            PrintComputerInfo(computer);
        }

        private static void CreateAndRemoveAComputer()
        {
            PrintAllComputers();
            AddNewComputer();
            PrintAllComputers();
            Console.ReadLine();
            Console.WriteLine("Undo changes");
            DatabaseContext.GetEntities().Undo();
            PrintAllComputers();
            Console.ReadLine();
            AddNewComputer();
            PrintAllComputers();
            Console.ReadLine();
            DatabaseContext.GetEntities().Save();
            DatabaseContext.GetEntities().Undo();
            PrintAllComputers();
        }

        private static void AddNewComputer()
        {
            var computer = new Computer()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = "iMac 24",
                Price = 1900,
                Description = "iMac 24' description",
                ImageFilename = "IMAC24.png"
            };
            DatabaseContext.GetEntities().Computers.Add(computer);
            Console.WriteLine("Added new Computer");
        }

        private static void PrintAllComputers()
        {
            var computers = QueryService.GetAllComputers();
            computers.ForEach(PrintComputerInfo);
        }

        private static void PrintComputerInfo(Computer computer)
        {
            if (computer == null)
            {
                Console.WriteLine("Computer is null");
                return;
            }
            Console.WriteLine($"Name: {computer.Name} Price: {computer.Price} ID: {computer.Guid}");
        }
        
        private static void PrintComputerInfo(ComputerInfoDto computer)
        {
            if (computer == null)
            {
                Console.WriteLine("Computer is null");
                return;
            }
            Console.WriteLine($"Name: {computer.Name} Price: {computer.Price} ID: {computer.Id}");
        }
    }
}