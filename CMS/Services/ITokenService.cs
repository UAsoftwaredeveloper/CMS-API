using Cms.Services.Models.Users;
using CMS.Models;
using System.Collections.Generic;

namespace CMS.Services
{
    public interface ITokenService
    {
        AuthUser GenerateToken(UsersModal user);
    }
}