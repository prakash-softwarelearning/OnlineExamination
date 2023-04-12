using OnlineExamination.Models;
using OnlineExamination.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<Technology>> BindTechDropDownList();
        Task<int> AddQuesAnswer(QuesAnsCreateDto quesAnsCreateDto);
        Task<int> AddTechnology(TechnologyDto technologyDto);
        Task<List<CandidateResultDto>> GetAllResult();
        Task<List<OnlineTestDto>> GetAllQuestionAnswer();
    }
}
