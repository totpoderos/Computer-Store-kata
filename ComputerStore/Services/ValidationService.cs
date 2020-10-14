using System;
using System.Linq;
using ComputerStore.Controllers.Request;

namespace ComputerStore.Services
{
    public class ValidationService
    {
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

        public static void ValidatedNewUserDto(NewUserDto newUserDto)
        {
            if (string.IsNullOrEmpty(newUserDto.Name)) throw new Exception("Name cannot be null or empty");
            if (string.IsNullOrEmpty(newUserDto.Surname)) throw new Exception("Surname cannot be null or empty");
            if (string.IsNullOrEmpty(newUserDto.Username)) throw new Exception("Username cannot be null or empty");
            if (string.IsNullOrEmpty(newUserDto.Password)) throw new Exception("Password cannot be null or empty");
            if (string.IsNullOrEmpty(newUserDto.Email)) throw new Exception("Email cannot be null or empty");
        }

        public static void ValidateUpdateUserDto(UpdateUserDto updatedUserDto)
        {
            if (string.IsNullOrEmpty(updatedUserDto.Name)) throw new Exception("Name cannot be null or empty");
            if (string.IsNullOrEmpty(updatedUserDto.Surname)) throw new Exception("Surname cannot be null or empty");
            if (string.IsNullOrEmpty(updatedUserDto.Username)) throw new Exception("Username cannot be null or empty");
            if (string.IsNullOrEmpty(updatedUserDto.Password)) throw new Exception("Password cannot be null or empty");
            if (string.IsNullOrEmpty(updatedUserDto.Email)) throw new Exception("Email cannot be null or empty");
        }
    }
}