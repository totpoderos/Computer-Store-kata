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
                throw ex;
            }
        }

        public void UpdateComputer(string computerId, UpdateComputerDto updateComputerDto)
        {
            ValidationService.ValidateUpdateComputerDto(updateComputerDto);
            Computer computer = QueryService.FindComputerByGuid(computerId);
            if (computer == null) throw new Exception("Computer not found. Id: " + computerId);
            computer.Name = updateComputerDto.Name;
            computer.Description = updateComputerDto.Description;
            computer.Price = updateComputerDto.Price;
            computer.ImageFilename = updateComputerDto.ImageFilename;
            DatabaseContext.GetEntities().Save();
        }

        public void DeleteComputer(string computerId)
        {
            Computer computer = QueryService.FindComputerByGuid(computerId);
            if (computer == null) throw new Exception("Computer not found. Id: " + computerId);
            Entities entities = DatabaseContext.GetEntities();
            entities.Computers.Remove(computer);
            entities.Save();
        }
    }
}