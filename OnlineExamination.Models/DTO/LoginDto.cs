using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Models.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string Password { get; set; }
    }
}
