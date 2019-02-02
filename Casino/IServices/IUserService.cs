using Casino.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Casino.IServices
{
    public interface IUserService
    {
        List<User> Get();
        User Get(string id);
        User Register(User user);
        User Login(User user);
    }
}
