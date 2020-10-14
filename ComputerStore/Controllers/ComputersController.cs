using System;
using System.Collections.Generic;
using ComputerStore.Controllers.Response;
using ComputerStore.Database;
using ComputerStore.Services;

namespace ComputerStore.Controllers
{
    public class ComputersController
    {
        public List<ComputerInfoDto> All()
        {
            var computers = QueryService.GetAllComputers();
            List<ComputerInfoDto> computersDtos = new List<ComputerInfoDto>();
            computers.ForEach(computer => computersDtos.Add(TransformationService.ComputerToResponseComputerDto(computer)));
            return computersDtos;
        }
        public ComputerInfoDto GetComputerInfo(string id)
        {
            var computer = QueryService.FindComputerByGuid(id);
            if (computer == null) throw new Exception("Computer not found, Id: " + id);
            return TransformationService.ComputerToResponseComputerDto(computer);
        }

        public ComputerInfoDto FindComputerByName(string name)
        {
            var computer = QueryService.FindComputerByName(name);
            if (computer == null) throw new Exception("Computer not found, name: " + name);
            return TransformationService.ComputerToResponseComputerDto(computer);
        }

        public ComputerImageInfoDto GetComputerImage(string guid)
        {
            var computer = QueryService.FindComputerByGuid(guid);
            if (computer == null) throw new Exception("Computer not found, guid: " + guid);
            return new ComputerImageInfoDto()
            {
                Content = ImageService.TransformTextoToAsciiMatrix(computer.Name)
            };
        }
    }
}