using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Cms.Services.Models.Users;
using CMS.Models;
using System.Runtime.CompilerServices;
using Cms.Services.Interfaces;
using Cms.Services.Models.MenuMaster;
using System.Linq;

namespace CMS.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRoleMenuPermissionService _roleMenuPermissionService;
        private readonly IMenuMasterService _menuMasterService;

        public TokenService(IConfiguration configuration, IMenuMasterService menuMasterService, IUserRoleMenuPermissionService userRoleMenuPermissionService)
        {
            _configuration = configuration;
            _roleMenuPermissionService = userRoleMenuPermissionService;
            _menuMasterService = menuMasterService;
        }
        public AuthUser GenerateToken(UsersModal user)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name.ToString(), user.UserName),
                        new Claim(ClaimTypes.NameIdentifier.ToString(), user.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier.ToString(), user.Id.ToString()),
                        new Claim(ClaimTypes.Role.ToString(), user.UserRole.Name),
                    };
                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"])),
                    signingCredentials: credentials
                );
                List<MenuMasterModal> menuMasterModal = new List<MenuMasterModal>();

                if (user.RoleId == 1)
                {
                    menuMasterModal = _menuMasterService.GetAllMenus(new Cms.Services.Filters.MenuMasterFilter
                    {
                        Active = true,
                        PageNumber = 1,
                        PageSize = int.MaxValue,
                    }).Result.Result;
                }
                else
                {
                    var roleMenuItems = _roleMenuPermissionService.GetAllUserRoleMenuPermission(new Cms.Services.Filters.UserRoleMenuPermissionFilter
                    {
                        Active = true,
                        RoleName = user.UserRole.Name,
                        PageNumber = 1,
                        PageSize = int.MaxValue,
                    }).Result;
                    if (roleMenuItems != null && roleMenuItems.Result != null && roleMenuItems.Result.Count > 0)
                    {
                        menuMasterModal = roleMenuItems.Result.Select(x => x.MenuMaster).ToList();
                    }
                }

                AuthUser authUser = new AuthUser();
                authUser.UserName = user.UserName;
                authUser.PhoneNumber = user.PhoneNumber;
                authUser.RoleId = user.RoleId;
                authUser.UserRole = user.UserRole;
                authUser.Active = true;
                authUser.Email = user.Email;
                authUser.Id = user.Id;
                authUser.MenuMasters = menuMasterModal;
                authUser.UserToken = new JwtSecurityTokenHandler().WriteToken(token);
                authUser.ExpiryTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"]));
                return authUser;
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                if (ex.Message == "409")
                    throw ex;
                else
                    throw ex;

            }
        }
    }
}
