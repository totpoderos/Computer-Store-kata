using System;
using ComputerStore.Controllers.Request;
using ComputerStore.Database;

namespace ComputerStore.Controllers
{
    public class ManagementController
    {
        public void AddComputer(AddComputerDto addComputerDto)
        {
            try
            {
                ValidationService.ValidateRequestComputerDto(addComputerDto);
                Computer computer = TransformationService.RequestComputerToComputerDto(addComputerDto);
                var entities = DatabaseContext.GetEntities();
                entities.Computers.Add(computer);
                entities.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}