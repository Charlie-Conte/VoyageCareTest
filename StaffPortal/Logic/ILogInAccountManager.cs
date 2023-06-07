#nullable disable
using Microsoft.AspNetCore.Identity;

namespace StaffPortal.Logic;

public interface ILogInAccountManager<TUser> where TUser : class
{
    Task<TUser> RegisterUser(TUser userToCreate);
}