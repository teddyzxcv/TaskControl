using System;
using System.Collections.Generic;
namespace TaskControl
{
    interface IAssignable
    {
        List<User> UserList { get; }
        void AddUser(User user);
        void DeleteUser(User user);
    }
}