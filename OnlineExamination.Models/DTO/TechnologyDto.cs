using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Models.DTO
{
    public class TechnologyDto
    {
        [Required(ErrorMessage = "Required.")]
        public string TechName { get; set; }
    }
}
