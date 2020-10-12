using System;
using ComputerStore.Controllers.Request;
using ComputerStore.Controllers.Response;

namespace ComputerStore
{
    public class ValidationService
    {
        //Validates an entity. These methods will be called from controllers to validate and entity before saving into repository
        public static void ValidateRequestComputerDto(AddComputerDto addComputerDto)
        {
            if (string.IsNullOrEmpty(addComputerDto.Id)) throw new Exception("Computer id cannot be null or empty");
            if (string.IsNullOrEmpty(addComputerDto.Name)) throw new Exception("Computer Name cannot be null or empty");
            if (string.IsNullOrEmpty(addComputerDto.Description)) throw new Exception("Computer Description cannot b.i null or empty");
            if (string.IsNullOrEmpty(addComputerDto.ImageFilename)) throw new Exception("Computer Filename cannot be null or empty");
            if (addComputerDto.Price < 0) throw new Exception("Computer price cannot be negative");
        }
    }
}