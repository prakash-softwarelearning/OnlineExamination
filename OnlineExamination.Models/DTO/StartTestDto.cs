using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnlineExamination.Models.EnumData;

namespace OnlineExamination.Models.DTO
{
    public class StartTestDto
    {
        [Required(ErrorMessage = "Required.")]
        public string Technology { get; set; }
        [Required(ErrorMessage = "Required.")]

        [EnumDataType(typeof(CandidateLevel))]
        public CandidateLevel QuestionLevel { get; set; }
    }
}
