using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Models
{
    public class CandidateResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string CandidateName { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Result { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string NoOfQuestions { get; set; }

        [Required(ErrorMessage = "Required.")]
        public int TotalQuestionEttempt { get; set; }

        [Required(ErrorMessage = "Required.")]
        public int CorrectAnswers { get; set; }
        public DateTime ExamDate { get; set; }

    }
}
