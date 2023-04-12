using OnlineExamination.Abstractions.Contract;
using OnlineExamination.Models;
using OnlineExamination.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.Repositorys.Implementation
{
    public class JobSeekerRepo : IJobSeeker
    {
        private readonly OnlineExaminationDBContext _context;
        public JobSeekerRepo(OnlineExaminationDBContext context)
        {
            _context = context;
        }

        public async Task AddCandidateResult(Result result)
        {
            var candResult = new CandidateResult() { 
               CandidateName = result.CandidateName,
               CorrectAnswers = result.CorrectAnswers,
               EmailId = result.EmailId,
               Result = result.Results,
               NoOfQuestions = result.NoOfQuestions,
               TotalQuestionEttempt = result.TotalQuestionEttempt,
               ExamDate = result.ExamDate,
            };
            await _context.CandidateResult.AddRangeAsync(candResult);
            await SaveChangesAsync();
        }

        public async Task<List<OnlineTestDto>> GetQuestionsByTechAndExperience(StartTestDto startTestDto)
        {
            return _context.Questions.Where(x=>x.Technology == startTestDto.Technology && x.QuestionLevel == (int)startTestDto.QuestionLevel)  //Outer Data Source
                         .Join( 
                         _context.Answer,  
                         question => question.QuestionsId,
                         answer => answer.QuestionsId,  
                         (question,answer) => new OnlineTestDto 
                         {
                             QuestionsId = question.QuestionsId,
                             QuestionsName = question.QuestionsName,
                             Option1 = question.Option1,
                             Option2 = question.Option2,
                             Option3 = question.Option3,
                             Option4 = question.Option4,
                             ExperienceShouldBe = question.ExperienceShouldBe,
                             QuestionLevel = question.QuestionLevel,
                             Technology=question.Technology,
                             AnswerName = answer.AnswerName
                         }).ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
