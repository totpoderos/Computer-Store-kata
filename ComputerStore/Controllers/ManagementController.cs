using System;
using ComputerStore.Controllers.Request;
using ComputerStore.Controllers.Response;
using ComputerStore.Database;
using ComputerStore.Domain;

namespace ComputerStore.Controllers
{
    public class ManagementController
    {
        public void AddComputer(AddComputerDto addComputerDto)
        {
            ValidationService.ValidateRequestComputerDto(addComputerDto);
            Computer computer = TransformationService.RequestComputerToComputerDto(addComputerDto);
            var entities = DatabaseContext.GetEntities();
            entities.Computers.Add(computer);
            entities.Save();
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

        public void DeleteOrder(string orderId)
        {
            Order order = QueryService.FindOrder(orderId);
            if (order == null) throw new Exception("Order not found. Id: " + orderId);
            Entities entities = DatabaseContext.GetEntities();
            entities.Orders.Remove(order);
            entities.Save();
        }

        public NewUserIdDto CreateUser(NewUserDto newUserDto)
        {
            ValidationService.ValidatedNewUserDto(newUserDto);
            if (QueryService.FinsUserByUsername(newUserDto.Username) != null) 
                throw new Exception("Cannot create user. Username is duplicate. Username: " + newUserDto.Username);
            User user = TransformationService.NewUserDtoToUser(newUserDto);
            Entities entities = DatabaseContext.GetEntities();
            entities.Users.Add(user);
            entities.Save();
            return new NewUserIdDto {Id = user.Guid};
        }

        public UserInfoDto GetUser(string userId)
        {
            User user = QueryService.FindUserById(userId);
            if (user == null) throw new Exception("User not found. Id: " + userId);
            return TransformationService.UserToUserInfoDto(user);
        }

        public void UpdateUser(string userId, UpdateUserDto updatedUserDto)
        {
            ValidationService.ValidateUpdateUserDto(updatedUserDto);
            User user = QueryService.FindUserById(userId);
            if (user == null) throw new Exception("User not found. Id: " + userId);
            user.Name = updatedUserDto.Name;
            user.Surname = updatedUserDto.Surname;
            user.Email = updatedUserDto.Email;
            user.Password = EncryptionService.HashPassword(updatedUserDto.Password);
            user.Username = updatedUserDto.Username;
            user.IsRoot = updatedUserDto.IsRoot;
            DatabaseContext.GetEntities().Save();
        }

        public void DeleteUser(string userId)
        {
            User user = QueryService.FindUserById(userId);
            if (user == null) throw new Exception("User not found. Id: " + userId);
            Entities entities = DatabaseContext.GetEntities();
            entities.Users.Remove(user);
            entities.Save();
        }
    }
}