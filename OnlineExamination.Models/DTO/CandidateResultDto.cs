using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Models.DTO
{
    public class CandidateResultDto
    {
        public int Id { get; set; }
        public string CandidateName { get; set; }
        public string EmailId { get; set; }

        public string Result { get; set; }

        public string NoOfQuestions { get; set; }

        public int TotalQuestionEttempt { get; set; }

        public int CorrectAnswers { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
