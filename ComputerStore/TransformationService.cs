using System;
using System.Linq;
using ComputerStore.Controllers;
using ComputerStore.Controllers.Request;
using ComputerStore.Controllers.Response;
using ComputerStore.Domain;

namespace ComputerStore
{
    public static class TransformationService
    {
        public static ComputerInfoDto ComputerToResponseComputerDto(Computer computer)
        {
            return new ComputerInfoDto()
            {
                Id = computer.Guid,
                Name = computer.Name,
                Description = computer.Description,
                Price = computer.Price
            };
        }

        public static Computer RequestComputerToComputerDto(AddComputerDto addComputerDto)
        {
            return new Computer
            {
                Guid = addComputerDto.Id,
                Name = addComputerDto.Name,
                Description = addComputerDto.Description,
                Price = addComputerDto.Price,
                ImageFilename = addComputerDto.ImageFilename
            };
        }

        public static OrderInformationDto OrderToOrderInfromationDto(Order order)
        {
            return new OrderInformationDto
            {
                OrderId = order.Guid,
                OrderDate = order.Date,
                ComputersGuid = order.Computers.Select(item => item.Guid).ToList()
            };
        }
    }
}