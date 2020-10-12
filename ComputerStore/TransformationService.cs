using System;
using ComputerStore.Controllers;
using ComputerStore.Controllers.Request;
using ComputerStore.Controllers.Response;

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
    }
}