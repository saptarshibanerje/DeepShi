using DeepShi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DeepShiShared;
using DeepShi.ViewModels;
using DeepShiShared.Models;

namespace DeepShi.Controllers
{
    public class ComingSoonController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISession _session;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiHelper _apiHelper;
        private readonly ILogger<ComingSoonController> _logger;
        readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        readonly IStringLocalizer<ComingSoonViewModel> _Localizer;

        public ComingSoonController(IConfiguration configuration,
               IHttpContextAccessor httpContextAccessor,
               IWebHostEnvironment webHostEnvironment,
               ILogger<ComingSoonController> logger,
               ApiHelper apiHelper, IStringLocalizer<SharedResource> sharedLocalizer, IStringLocalizer<ComingSoonViewModel> Localizer) : base(configuration, httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            _webHostEnvironment = webHostEnvironment;
            _apiHelper = apiHelper;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger;
            _Localizer = Localizer;
        }

        public IActionResult Index(ComingSoonViewModel model)
        {
            return View(model);
        }
        [HttpPost]
        public IActionResult GetNotified(ComingSoonViewModel model)
        {
            string returnMessage = "";

            if (ModelState.IsValid)
            {
                returnMessage = WriteToTextFile("\n" + DateTime.Now.ToString("dd/MM/yyyy") + " From IP: " + _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString() + "---> Name: " + model.Name + ". Mobile No: " + model.MobileNo.ToString());
            }
            AppNotification.ShowMessage(this, returnMessage, MessageType.Success);
            return RedirectToAction("Index");
        }

        #region Private Method

        private string WriteToTextFile(string Message)
        {
            string returnMessage = "";
            try
            {

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "GetNotified");// 
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Append text to an existing file named "WriteLines.txt".
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(uploadFolder, "GetNotifiedList.txt"), true))
                {
                    outputFile.WriteLine(Message);
                }
                returnMessage = "Successfully register for launch notification.....";
            }
            catch (Exception ex)
            {
                returnMessage = "Failed to take register yourself for launch notification!!!";
            }

            return returnMessage;
        }
        #endregion

    }
}
