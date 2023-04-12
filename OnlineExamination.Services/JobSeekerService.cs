using OnlineExamination.Abstractions.Contract;
using OnlineExamination.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Services
{
    public class JobSeekerService : IJobSeekerService
    {
        private readonly IJobSeeker _jobSeeker;
        public JobSeekerService(IJobSeeker jobSeeker)
        {
            _jobSeeker = jobSeeker;
        }

        public async Task AddCandidateResult(Result result)
        {
           await _jobSeeker.AddCandidateResult(result);
        }

        public async Task<List<OnlineTestDto>> GetQuestionsByTechAndExperience(StartTestDto startTestDto)
        {
            return await _jobSeeker.GetQuestionsByTechAndExperience(startTestDto);
        }
    }
}
