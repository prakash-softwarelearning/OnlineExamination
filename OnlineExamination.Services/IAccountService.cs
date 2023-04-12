using OnlineExamination.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Services
{
    public interface IAccountService
    {
        Task<RoleDto> GetUserData(string userName);
        Task<bool> AuthanticateUser(LoginDto loginDto);
    }
}
