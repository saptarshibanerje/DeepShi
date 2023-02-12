using DeepShi.Models;
using DeepShiShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepShi.Controllers
{
    [AllowAnonymous]
    public class ErrorController : BaseController
    {
        private readonly ISession _session;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApiHelper _apiHelper;
        private readonly ILogger<ErrorController> _logger;
        readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public ErrorController(IConfiguration configuration,
               IHttpContextAccessor httpContextAccessor,
               IWebHostEnvironment webHostEnvironment,
               ILogger<ErrorController> logger,
               ApiHelper apiHelper, IStringLocalizer<SharedResource> sharedLocalizer) : base(configuration, httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            _webHostEnvironment = webHostEnvironment;
            _apiHelper = apiHelper;
            _logger = logger;
            _sharedLocalizer = sharedLocalizer;
        }
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            ErrorViewModel model = new ErrorViewModel();

            switch (statusCode)
            {
                case 404:
                    model.RequestId = statusCode.ToString();
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    break;
                case 400:
                    model.RequestId = statusCode.ToString();
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    break;
                case 403:
                    model.RequestId = statusCode.ToString();
                    ViewBag.ErrorMessage = "You are unauthorised to see the content. Please login.";
                    break;
                case 1001:
                    model.RequestId = "1001";
                    ViewBag.ErrorMessage = "Your Email has not been confirmed";
                    break;
            }

            return View("Error", model);
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ErrorMessage = exceptionDetails.Error.Message;
            ViewBag.StackTrace = exceptionDetails.Error.StackTrace;
            return View("ExError");
        }
    }
}
