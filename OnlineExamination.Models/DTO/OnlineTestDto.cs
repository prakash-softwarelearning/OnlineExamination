using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnlineExamination.Models.EnumData;

namespace OnlineExamination.Models.DTO
{
    public class OnlineTestDto
    {
        public int QuestionsId { get; set; }
        public string QuestionsName { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string AnswerName { get; set; }
        public string Technology { get; set; }
        public int QuestionLevel { get; set; }
        public string ExperienceShouldBe { get; set; }
        public string SelectedAnswer { get; set; }
        public CandidateLevel QuestionLevelType { get; set; }
    }
}
