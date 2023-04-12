using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineExamination.Models
{
    public class Questions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionsId { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string QuestionsName { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Technology { get; set; }
        [Required(ErrorMessage = "Required.")]
        public int QuestionLevel { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string ExperienceShouldBe { get; set; }
       public DateTime CreatedOn { get; set; }
       public DateTime UpdatedOn { get; set; }
    }
}
