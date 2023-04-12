using OnlineExamination.Models;
using OnlineExamination.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Abstractions.Contract
{
    public interface IJobSeeker
    {
        Task<List<OnlineTestDto>> GetQuestionsByTechAndExperience(StartTestDto startTestDto);
        Task AddCandidateResult(Result result);
    }
}
