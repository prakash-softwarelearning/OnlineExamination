using Abstraction;
using OnlineExamination.Models;
using OnlineExamination.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LoginRepo : ILoginRepo
    {
        private readonly OnlineExaminationDBContext _context;
        public LoginRepo(OnlineExaminationDBContext context)
        {
            this._context = context;
        }
        public async Task<bool> AuthanticateUser(LoginDto loginDto)
        {
            return _context.UserLogin.Where(x => x.Username.Trim().ToLower() == loginDto.UserName.Trim().ToLower() && x.Password == loginDto.Password).Any();
        }

        public async Task<RoleDto> GetUserData(string userName)
        {
            return  _context.Roles.Where(x => x.UserName.Trim().ToLower() == userName.Trim().ToLower()).Select(z=> new RoleDto { UserName = z.UserName,Email = z.Email,RoleName=z.RoleName  }).FirstOrDefault();
        }
    }
}
