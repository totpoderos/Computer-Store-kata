using System;
using System.Linq;
using System.Runtime.CompilerServices;
using ComputerStore.Controllers.Request;
using ComputerStore.Controllers.Response;
using ComputerStore.Domain;

namespace ComputerStore
{
    public class ValidationService
    {
        //Validates an entity. These methods will be called from controllers to validate and entity before saving into repository
        public static void ValidateRequestComputerDto(AddComputerDto addComputerDto)
        {
            if (string.IsNullOrEmpty(addComputerDto.Name)) throw new Exception("Computer Name cannot be null or empty");
            if (string.IsNullOrEmpty(addComputerDto.Description)) throw new Exception("Computer Description cannot be null or empty");
            if (string.IsNullOrEmpty(addComputerDto.ImageFilename)) throw new Exception("Computer Filename cannot be null or empty");
            if (addComputerDto.Price < 0) throw new Exception("Computer price cannot be negative");
        }

        public static void ValidateNewOrderDto(NewOrderDto newOrderDto)
        {
            newOrderDto.OrderLines.ForEach(item =>
            {
                if (string.IsNullOrEmpty(item.ComputerId)) throw new Exception("Computer id cannot be null or empty");
                if (item.Quantity < 0) throw new Exception("Computer quantity cannot be negative");
            });
        }

        public static void ValidateAddComputersToOrderDto(AddComputersToOrderDto computersToOrderDto)
        {
            computersToOrderDto.OrderLines.ToList().ForEach(orderLine =>
            {
                if (string.IsNullOrEmpty(orderLine.ComputerId)) throw new Exception("Computer id cannot be null or empty");
                if (orderLine.Quantity < 0) throw new Exception("Computer quantity cannot be negative");
            });
        }

        public static void ValidateUpdateComputerDto(UpdateComputerDto updateComputerDto)
        {
            if (string.IsNullOrEmpty(updateComputerDto.Name)) throw new Exception("Computer Name cannot be null or empty");
            if (string.IsNullOrEmpty(updateComputerDto.Description)) throw new Exception("Computer Description cannot be null or empty");
            if (string.IsNullOrEmpty(updateComputerDto.ImageFilename)) throw new Exception("Computer Filename cannot be null or empty");
            if (updateComputerDto.Price < 0) throw new Exception("Computer price cannot be negative");
        }
    }
}