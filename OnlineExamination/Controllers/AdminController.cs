using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using OnlineExamination.Controllers;
using OnlineExamination.Models;
using OnlineExamination.Models.DTO;
using OnlineExamination.Services;
using System.Drawing;
using static OnlineExamination.Models.EnumData;

namespace OnlineExaminationWeb.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IAdminService _adminService;

        public AdminController(ILogger<AccountController> logger, IAdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;
        }

        public async Task<ActionResult> Index()
        {
            var userInfo = JsonConvert.DeserializeObject<Roles>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserName = userInfo.UserName;
            return View();
        }

        public async Task<ActionResult> QuesAnsCreate()
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
            ViewBag.Tech = new SelectList(await _adminService.BindTechDropDownList(), "Id", "TechName");
            if (TempData["DataSaved"] != null)
            {
                ViewBag.DataSaved = TempData["DataSaved"].ToString();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> QuesAnsCreate(QuesAnsCreateDto quesAnsCreateDto)
        {
            var userInfo = JsonConvert.DeserializeObject<Roles>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserName = userInfo.UserName;
            if (!ModelState.IsValid)
            {
                var enumData = from CandidateLevel e in Enum.GetValues(typeof(CandidateLevel))
                               select new
                               {
                                   ID = (int)e,
                                   Name = e.ToString()
                               };
                ViewBag.EnumList = new SelectList(enumData, "ID", "Name");
                ViewBag.Tech = new SelectList(await _adminService.BindTechDropDownList(), "Id", "TechName");
                return View();
            }
            int isDataPushed = await _adminService.AddQuesAnswer(quesAnsCreateDto);
            if (isDataPushed > 0)
            {
                TempData["DataSaved"] = "Saved Successfully";
            }

            return RedirectToAction("QuesAnsCreate");
        }

        public async Task<ActionResult> AddTechnology()
        {
            var userInfo = JsonConvert.DeserializeObject<Roles>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserName = userInfo.UserName;
            if (TempData["TechDataSave"] != null)
            {
                ViewBag.TechDataSave = TempData["TechDataSave"].ToString();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTechnology(TechnologyDto technologyDto)
        {
            var userInfo = JsonConvert.DeserializeObject<Roles>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserName = userInfo.UserName;
            if (!ModelState.IsValid) return View();
            await _adminService.AddTechnology(technologyDto);
            TempData["TechDataSave"] = "Data Saved Successfully";
            return RedirectToAction("AddTechnology");
        }

        public async Task<ActionResult> CandiateResult()
        {
            var userInfo = JsonConvert.DeserializeObject<Roles>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserName = userInfo.UserName;
            var canResult = await _adminService.GetAllResult();
            return View(canResult);
        }

        public async Task<ActionResult> ViewQuestionAnswer()
        {
            DropdownUser();
            var viewQuestionAns =  _adminService.GetAllQuestionAnswer();
            return View(viewQuestionAns.Result);
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
