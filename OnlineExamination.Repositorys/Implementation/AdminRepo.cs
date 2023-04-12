using Microsoft.EntityFrameworkCore;
using OnlineExamination.Abstractions.Contract;
using OnlineExamination.Models;
using OnlineExamination.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static OnlineExamination.Models.EnumData;

namespace OnlineExamination.Repositorys.Implementation
{
    public class AdminRepo : IAdminRepo
    {
        private readonly OnlineExaminationDBContext _context;
        

        public AdminRepo(OnlineExaminationDBContext context)
        {
                _context = context;
        }
        public async Task<IEnumerable<Technology>> BindTechDropDownList()
        {
            return _context.Technology.AsNoTracking();
        }

        public async Task<int> AddQuesAnswer(QuesAnsCreateDto quesAnsCreateDto)
        {
            var question = new Questions()
            { QuestionsName = quesAnsCreateDto.QuestionsName,
              QuestionLevel = (int)quesAnsCreateDto.QuestionLevel,
              ExperienceShouldBe = quesAnsCreateDto.ExperienceShouldBe,
              Option1 = quesAnsCreateDto.Option1,
              Option2 = quesAnsCreateDto.Option2,
              Option3 = quesAnsCreateDto.Option3,
              Option4 = quesAnsCreateDto.Option4,
              Technology = quesAnsCreateDto.Technology,
              CreatedOn = DateTime.UtcNow
            };
            await _context.Questions.AddRangeAsync(question);
            await SaveChangesAsync();
            var questionId = _context.Questions.Find(_context.Questions.Max(p => p.QuestionsId)).QuestionsId;
            var answer = new Answer()
            {
                AnswerName = quesAnsCreateDto.AnswerName,
                CreatedOn = DateTime.UtcNow,
                QuestionsId = questionId
            };
            await _context.Answer.AddRangeAsync(answer);
            await SaveChangesAsync();
            return questionId;
        }

        public async Task<int> AddTechnology(TechnologyDto technologyDto)
        {
            var technology = new Technology() { TechName = technologyDto.TechName };
            await _context.Technology.AddRangeAsync(technology);
            await SaveChangesAsync();
            return _context.Technology.Find(_context.Technology.Max(p => p.Id)).Id;
           
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<CandidateResultDto>> GetAllResult()
        {
           return await _context.CandidateResult.Select(x=> new CandidateResultDto { Id = x.Id, CandidateName = x.CandidateName,
           CorrectAnswers = x.CorrectAnswers,EmailId=x.EmailId,NoOfQuestions = x.NoOfQuestions,Result = x.Result,ExamDate = x.ExamDate
           ,TotalQuestionEttempt = x.TotalQuestionEttempt}).OrderByDescending(x=>x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<OnlineTestDto>> GetAllQuestionAnswer()
        {
            return (from a in _context.Questions
                    join c in _context.Answer on a.QuestionsId equals c.QuestionsId into lg
                    join d in _context.Technology on a.Technology equals d.Id.ToString()
                    from x in lg.DefaultIfEmpty()
                    select new OnlineTestDto
                    {
                        QuestionsId = a.QuestionsId,
                        QuestionsName = a.QuestionsName,
                        Option1 = a.Option1,
                        Option2 = a.Option2,
                        Option3 = a.Option3,
                        Option4 = a.Option4,
                        ExperienceShouldBe = a.ExperienceShouldBe,
                        QuestionLevel = a.QuestionLevel,
                        QuestionLevelType = (CandidateLevel)a.QuestionLevel,
                        Technology = d.TechName,
                        AnswerName = x.AnswerName
                    }).AsNoTracking().ToList();

        }
    }
}
