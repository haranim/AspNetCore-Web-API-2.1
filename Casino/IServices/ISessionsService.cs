using Casino.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Casino.IServices
{
    public interface ISessionsService
    {
        Session CreateSession(Games game, User user);
    }
}
