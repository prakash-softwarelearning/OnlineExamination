using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Models.DTO
{
    public class ViewQuestionAnswer
    {
        public List<OnlineTestDto> onlineTestDtos { get; set; }
        public StartTestDto startTestDto { get; set; }
    }
}
