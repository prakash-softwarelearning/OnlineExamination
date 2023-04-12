using OnlineExamination.Models.DTO;
using OnlineExamination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineExamination.Abstractions.Contract;
using Abstraction;

namespace OnlineExamination.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;

        public AdminService(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }
        public Task<IEnumerable<Technology>> BindTechDropDownList()
        {
            return _adminRepo.BindTechDropDownList();
        }

        public Task<int> AddQuesAnswer(QuesAnsCreateDto quesAnsCreateDto)
        {
            return _adminRepo.AddQuesAnswer(quesAnsCreateDto);
        }

        public Task<int> AddTechnology(TechnologyDto technologyDto)
        {
            return _adminRepo.AddTechnology(technologyDto);
        }

        public async Task<List<CandidateResultDto>> GetAllResult()
        {
            return await _adminRepo.GetAllResult();
        }

        public Task<List<OnlineTestDto>> GetAllQuestionAnswer()
        {
            return _adminRepo.GetAllQuestionAnswer();
        }
    }
}
