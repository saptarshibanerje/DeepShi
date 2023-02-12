using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DeepShiShared;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.DataProtection;

namespace DeepShi
{
    [Authorize(AuthorizationType.User)]
    public partial class BaseController : Controller
    {
        protected const string ApplicationTypeJson = "application/json";

        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public BaseController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            //httpContextAccessor.HttpContext.Request. _session.UserAuthToken();
            var cultureInfo = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            CultureInfo newCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            DateTimeFormatInfo objDateTimeFormatInfo = new DateTimeFormatInfo
            {
                ShortDatePattern = "dd/MM/yyyy",
                DateSeparator = "/"
            };
            newCulture.DateTimeFormat = objDateTimeFormatInfo;
            Thread.CurrentThread.CurrentCulture = newCulture;
            
        }

        public override RedirectResult Redirect(string url)
        {
            return base.Redirect(url);
        }
    }
}
