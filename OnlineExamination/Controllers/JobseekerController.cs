using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using OnlineExamination.Controllers;
using OnlineExamination.Models;
using OnlineExamination.Models.DTO;
using OnlineExamination.Services;
using static OnlineExamination.Models.EnumData;

namespace OnlineExaminationWeb.Controllers
{
    [Authorize(Policy = "JobSeekerOnly")]
    public class JobseekerController : Controller
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IJobSeekerService _jobSeekerService;
        private readonly IAdminService _adminService;
        private IConfiguration _configuration;

        public JobseekerController(ILogger<AccountController> logger, IJobSeekerService jobSeekerService, IConfiguration configuration, IAdminService adminService)
        {
            _logger = logger;
            _jobSeekerService = jobSeekerService;
            _configuration = configuration;
            _adminService = adminService;
            var enumData = from CandidateLevel e in Enum.GetValues(typeof(CandidateLevel))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };
            ViewBag.EnumList = new SelectList(enumData, "ID", "Name");
            
        }

        public async Task<ActionResult> Index()
        {
            DropdownUser();
            if (TempData["Notification"] != null) { 
            ViewBag.Notification = TempData["Notification"].ToString(); 
            }
            ViewBag.Tech = new SelectList(await _adminService.BindTechDropDownList(), "Id", "TechName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(StartTestDto startTestDto)
        {
            if (!ModelState.IsValid)
            {
                DropdownUser();
                ViewBag.Tech = new SelectList(await _adminService.BindTechDropDownList(), "Id", "TechName");
                return View();
            }
            DropdownUser();
            ViewBag.Tech = new SelectList(await _adminService.BindTechDropDownList(), "Id", "TechName");
            TempData["question"] = JsonConvert.SerializeObject(startTestDto);
            return RedirectToAction("OnlineTest");
        }


        public async Task<ActionResult> OnlineTest()
        {
            var userInfo = JsonConvert.DeserializeObject<Roles>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserName = userInfo.UserName;
            var appData = _configuration.GetSection("ExaminationTimeSetting").Get<ExaminationTimeSetting>();
            ViewBag.minutes = appData.Time;
            var questions = JsonConvert.DeserializeObject<StartTestDto>(TempData["question"].ToString());
            TempData.Keep("question");
            var getQuestions = await _jobSeekerService.GetQuestionsByTechAndExperience(questions);
            if (getQuestions.Count == 0)
            {
                TempData["Notification"] = "Question is not found";
                return RedirectToAction("Index");
            }
            List<OnlineTestDto> obj = new List<OnlineTestDto>();
            obj.AddRange(getQuestions);
            TempData["QuesWOperation"] = JsonConvert.SerializeObject(getQuestions);
            TempData.Keep("QuesWOperation");
            ViewBag.TotalNoQuestion = getQuestions.Count();
            TempData["TotalNoQuestion"] = ViewBag.TotalNoQuestion;
            TempData.Keep("TotalNoQuestion");
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnlineTest(List<OnlineTestDto> onlineTestViewModel)
        {
            var QuesWOperation = JsonConvert.DeserializeObject<List<OnlineTestDto>>(TempData["QuesWOperation"].ToString());
            Int32 totalNoOFQuestion = Convert.ToInt32(TempData["TotalNoQuestion"].ToString());
            Int32 correctAns = 0; Int32 tQettempt = 0;
            if (onlineTestViewModel != null && onlineTestViewModel.Count() > 0)
            {
                foreach (var item in onlineTestViewModel)
                {
                    if (item.SelectedAnswer != null)
                    {
                        if (item.AnswerName.Trim().ToLower() == item.SelectedAnswer.Trim().ToLower())
                        {
                            correctAns++;
                            tQettempt++;

                        }
                        else
                        {
                            tQettempt++;
                        }
                    }                           
                    else {
                        tQettempt++;
                    }
                }
            }
            var userInfo = JsonConvert.DeserializeObject<Roles>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserName = userInfo.UserName;
            var calPercentage = (correctAns * (100 / totalNoOFQuestion));
            Result results = new Result()
            {
                CorrectAnswers = correctAns,
                TotalQuestionEttempt = tQettempt,
                NoOfQuestions = totalNoOFQuestion.ToString(),
                ExamDate = DateAndTime.Now,
                CandidateName = userInfo.UserName,
                EmailId = userInfo.Email,
                Results = calPercentage >= 50 ? CandiateResult.Pass.ToString() : CandiateResult.Fail.ToString(),
                Percentage = Convert.ToString(calPercentage) + '%'
            };

            return RedirectToAction("Result", results);
        }

        public async Task<ActionResult> Result(Result result)
        {
            if (result != null)
            {
               await _jobSeekerService.AddCandidateResult(result);
            }
            DropdownUser();
          return View(result);
        }

        private void DropdownUser()
        {
            var userInfo = JsonConvert.DeserializeObject<Roles>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserName = userInfo.UserName;
            var enumData = from CandidateLevel e in Enum.GetValues(typeof(CandidateLevel))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };
            ViewBag.EnumList = new SelectList(enumData, "ID", "Name");
         }
    }
}
