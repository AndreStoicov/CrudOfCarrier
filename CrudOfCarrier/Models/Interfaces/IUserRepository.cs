using CrudOfCarrier.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudOfCarrier.Models.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsValid(string userName, string pass);

        Task<User> Register(User user);

        Task<User> GetByName(string userName);
    }
}