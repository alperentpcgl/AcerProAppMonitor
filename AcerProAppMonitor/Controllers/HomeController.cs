using AcerProAppMonitor.Models;
using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Diagnostics;

namespace AcerProAppMonitor.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITargetAppService _targetAppService;

        public HomeController(ITargetAppService targetAppService)
        {
            _targetAppService = targetAppService;
     
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult TargetAppList()
        {
            var result = _targetAppService.GetList();
            return Json(new { data = result.Data });
        }

        public IActionResult AddOrEdit(int id=0)
        {
            if (id == 0)
                return View(new TargetAppDto());
            else
            {
                var result = _targetAppService.GetById(id);
                return View(result.Data);
            }
            
        }
        [HttpPost]
        public IActionResult AddOrEdit(TargetAppDto model)
        {
            if(model.Id==0)
            {
               var result= _targetAppService.Add(model);
                return Json(result.Success);
            }
            else
            {
                var result=_targetAppService.Edit(model);
                return Json(result.Success);
            }
           
        }

        public IActionResult Delete(int id)
        {
            var result=_targetAppService.Delete(id);
            return Json(result.Success);
        }
       


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}