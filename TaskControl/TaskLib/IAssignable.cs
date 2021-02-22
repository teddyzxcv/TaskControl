using System;
using System.Collections.Generic;
namespace TaskControl
{
    /// <summary>
    /// Interface for user.
    /// </summary>
    interface IAssignable
    {

        List<User> UserList { get; }
        /// <summary>
        /// Add user for userlist.
        /// </summary>
        /// <param name="user"></param>
        void AddUser(User user);
        /// <summary>
        /// Delete user for userlist.
        /// </summary>
        /// <param name="user"></param>
        void DeleteUser(User user);
    }
}