using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Models.DTO
{
    public class Result
    {
        public int Id { get; set; }
        public string CandidateName { get; set; }
        public string EmailId { get; set; }

        public string Results { get; set; }

        public string NoOfQuestions { get; set; }

        public int TotalQuestionEttempt { get; set; }

        public int CorrectAnswers { get; set; }
        public DateTime ExamDate { get; set; }

        public string Percentage { get; set; }
    }
}
