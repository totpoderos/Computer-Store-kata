using System;
using System.Linq;
using ComputerStore.Controllers.Request;
using ComputerStore.Controllers.Request.Computer;
using ComputerStore.Controllers.Request.Order;
using ComputerStore.Controllers.Request.User;
using ComputerStore.Controllers.Response;
using ComputerStore.Controllers.Response.Computer;
using ComputerStore.Controllers.Response.Order;
using ComputerStore.Controllers.Response.User;
using ComputerStore.Database;
using ComputerStore.Domain;

namespace ComputerStore.Services
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
                Price = computer.Price,
                ImageFilename = computer.ImageFilename
            };
        }

        public static Computer RequestComputerToComputerDto(AddComputerDto addComputerDto)
        {
            return new Computer
            {
                Guid = Guid.NewGuid().ToString(),
                Name = addComputerDto.Name,
                Description = addComputerDto.Description,
                Price = addComputerDto.Price,
                ImageFilename = addComputerDto.ImageFilename
            };
        }

        public static OrderInformationDto OrderToOrderInformationDto(Order order)
        {
            return new OrderInformationDto
            {
                OrderId = order.Guid,
                OrderDate = order.Date,
                OrderLinesInfo = order.OrderLines.Select(OrderLineToOrderLineInfoDto).ToList(),
                Price = order.OrderLines.Aggregate((Decimal)0, (price, line) => price + line.Quantity * line.Computer.Price )
            };
        }

        private static OrderLineInfoDto OrderLineToOrderLineInfoDto(OrderLine orderLine)
        {
            return new OrderLineInfoDto
            {
                ComputerId = orderLine.Computer.Guid,
                Quantity = orderLine.Quantity
            };
        }

        public static OrderLine NewOrderLineDtoToOrderLine(NewOrderLineDto newOrderLineDto)
        {
            return new OrderLine
            {
                Computer = QueryService.FindComputerByGuid(newOrderLineDto.ComputerId) ?? throw new Exception("Computer not found. Id: " + newOrderLineDto.ComputerId),
                Quantity = newOrderLineDto.Quantity
            };
        }

        public static OrderLine AddOrderLineToOrderLine(AddOrderLineDto orderLineDto)
        {
            Computer computer = QueryService.FindComputerByGuid(orderLineDto.ComputerId);
            if (computer == null)
                throw new Exception("Cannot add computer to Order, Computer not found Id: " + orderLineDto.ComputerId);
            if (orderLineDto.Quantity < 0) throw new Exception("Quantity cannot be negative");
            var orderLine = new OrderLine
            {
                Computer = computer,
                Quantity = orderLineDto.Quantity
            };
            return orderLine;
        }

        public static User NewUserDtoToUser(NewUserDto newUserDto)
        {
            string encryptedPassword = EncryptionService.HashPassword(newUserDto.Password);
            string userGuid = Guid.NewGuid().ToString();
            return new User
            {
                Guid = userGuid,
                Name = newUserDto.Name,
                Surname = newUserDto.Surname,
                Username = newUserDto.Username,
                Password = encryptedPassword,
                IsRoot = newUserDto.IsRoot,
                Email = newUserDto.Email
            };
        }

        public static UserInfoDto UserToUserInfoDto(User user)
        {
            return new UserInfoDto
            {
                Id = user.Guid,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.Username,
                IsRoot = user.IsRoot
            };
        }
    }
}