using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static OnlineExamination.Models.EnumData;

namespace OnlineExamination.Models.DTO
{
    public class QuesAnsCreateDto
    {
        [Required(ErrorMessage = "Required.")]
        public string QuestionsName { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string Option1 { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string Option2 { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string AnswerName { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Technology { get; set; }
        [Required(ErrorMessage = "Required.")]

        [EnumDataType(typeof(CandidateLevel))]
        public CandidateLevel QuestionLevel { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string ExperienceShouldBe { get; set; }
    }
}
