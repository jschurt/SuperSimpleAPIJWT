using SuperSimpleAPIJWT.Models;
using System.Collections.Generic;
using System.Linq;

namespace SuperSimpleAPIJWT.FakeRepositories
{
    public static class FakeUserRepository
    {
        public static User Get(string username, string password)
        {

            var users = new List<User>();
            users.Add(new User { Id = 1, UserName = "Julio", Password = "123", Role = "employee" });
            users.Add(new User { Id = 2, UserName = "Gabriel", Password = "123", Role = "manager" });

            return users.Where(u => u.UserName.ToLower() == username.ToLower() && u.Password == password).FirstOrDefault();

        } //Get
    }
}
