using System.Collections.Generic;
using System.Linq;
using ComputerStore.Domain;

namespace ComputerStore.Database
{
    public static class QueryService
    {
        //Execute database queries
        public static List<Computer> GetAllComputers()
        {
            return DatabaseContext.GetEntities().Computers;
        }

        public static Computer FindComputerByName(string computerName)
        {
            var entities = DatabaseContext.GetEntities();
            var query = from computer in entities.Computers
                where computer.Name.ToUpper() == computerName.ToUpper()
                select computer;
            return query.FirstOrDefault();
        }

        public static Computer FindComputerByGuid(string guid)
        {
            var entities = DatabaseContext.GetEntities();
            var query = from computer in entities.Computers
                where computer.Guid.ToUpper().Equals(guid.ToUpper())
                select computer;
            return query.FirstOrDefault();
        }

        public static Order FindOrder(string id)
        {
            var entities = DatabaseContext.GetEntities();
            var query = from order in entities.Orders
                where order.Guid.ToUpper() == id.ToUpper()
                select order;
            return query.FirstOrDefault();
        }

        public static List<Order> GetAllOrders()
        {
            var entities = DatabaseContext.GetEntities();
            return entities.Orders;
        }

        public static User FindUserById(string userId)
        {
            var entities = DatabaseContext.GetEntities();
            var query = from user in entities.Users
                where user.Guid.ToUpper().Equals(userId.ToUpper())
                select user;
            return query.FirstOrDefault();
        }

        public static User FinsUserByUsername(string username)
        {
            var entities = DatabaseContext.GetEntities();
            var query = from user in entities.Users
                where user.Username.ToUpper().Equals(username.ToUpper())
                select user;
            return query.FirstOrDefault();
        }
    }
}