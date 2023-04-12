using Abstraction;
using OnlineExamination.Abstractions.Contract;
using OnlineExamination.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILoginRepo _login;

        public AccountService(ILoginRepo login)
        {
            _login = login;
        }
        public Task<bool> AuthanticateUser(LoginDto loginDto)
        {
            return _login.AuthanticateUser(loginDto);
        }

        public Task<RoleDto> GetUserData(string userName)
        {
            return _login.GetUserData(userName);
        }
    }
}
