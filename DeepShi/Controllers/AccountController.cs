using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DeepShiApp.ViewModels;
using DeepShiShared;
using DeepShiShared.Models;
using System;
using DeepShiApp;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Localization;
using System.Net.Http.Json;
using DeepShiApp.Helpers;
using System.Net.Mail;
using System.IO;
using System.Net;
using DeepShiShared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using System.Web;
using System.Collections.Generic;
using DeepShiEntityModels.Models;

namespace DeepShiApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly ISession _session;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApiHelper _apiHelper;
        private readonly ILogger<AccountController> _logger;
        private readonly ILogger<EmailHelper> _elogger;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IDataProtector _protector;
        public AccountController(IConfiguration configuration,
               IHttpContextAccessor httpContextAccessor,
               IWebHostEnvironment webHostEnvironment,
               ILogger<AccountController> logger,
               ILogger<EmailHelper> elogger,
               ApiHelper apiHelper, IStringLocalizer<SharedResource> sharedLocalizer, IDataProtectionProvider dataProtectionprotector) : base(configuration, httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            _webHostEnvironment = webHostEnvironment;
            _apiHelper = apiHelper;
            _logger = logger;
            _elogger = elogger;
            _sharedLocalizer = sharedLocalizer;
            _protector = dataProtectionprotector.CreateProtector(configuration.GetSection("AppConfig")["ProtectionString"]);
        }

        #region Register New Users

        [HttpPost]
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> IsEmailInUse(string EmailId)
        {
            using HttpResponseMessage httpResponse = await _apiHelper.ApiClient.GetAsync(ApiDictionary.UserApiEmailExistsOrNot + "?email=" + EmailId);

            if (httpResponse.IsSuccessStatusCode)
            {
                ApiResponse apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse>();
                if (apiResponse.Status == 1)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"Email {EmailId} is already in use");
                }
            }
            else
            {
                return Json($"Could not validate {EmailId} .");

            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Appusers.Password = UtilityHelper.RandomString(8);
                    if (!string.IsNullOrEmpty(model.EmailId))
                    {
                        model.Appusers.EmailId = model.EmailId;
                    }
                    using HttpResponseMessage httpResponse = await _apiHelper.ApiClient.PostAsJsonAsync(ApiDictionary.UserApiRegisterUser, model.Appusers);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        ApiResponse<string> apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<string>>();
                        if (apiResponse.Status == 1)
                        {
                            EmailHelper emailHelper = new EmailHelper(_configuration, _elogger);

                            string _EmailBody = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "EmailTemplate/ConfirmEmail.txt"));
                            var sublink = _protector.Protect("userid=" + apiResponse.Data + "&token=" + apiResponse.Message.Code);
                            var link = Url.Action("ConfirmEmail", "Account", new { token = sublink }, Request.Scheme);
                            _EmailBody = _EmailBody.Replace("_UserPassword", model.Appusers.Password).Replace("_UserUrl", link);

                            bool isMailSent = await emailHelper.SendEmail(new EmailModel { EmailTo = model.EmailId, EmailBody = _EmailBody, EmailSubject = "DeepShi Registratioin", EmailBcc = "", EmailCc = "", IsEmailBodyHtml = true });
                            //if (!isMailSent)
                            //{
                            //    throw Exception("Email Not send!!");
                            //}
                            AppNotification.ShowMessage(this, _sharedLocalizer["RegistrationSuccess"], MessageType.Success);
                            if (model.RegisterBy.ToLower() == "admin")
                            {
                                return Redirect($"/Account/UserList");
                            }
                            return Redirect($"/Account/Login");
                        }
                        else
                        {
                            AppNotification.ShowMessage(this, _sharedLocalizer["AuthenticationFailure"], MessageType.Error);
                        }
                    }
                    else
                    {
                        return Redirect($"Error/{(int)httpResponse.StatusCode}");

                    }
                }
                else
                {
                    AppNotification.ShowMessage(this, _sharedLocalizer["ValidationErrorTitle"], "", MessageType.Error);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return BadRequest(ex);
            }
            return View(model);//
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            string username = _session.UserId();
            ApiResponse<List<RegisterModel>> users = new ApiResponse<List<RegisterModel>>();
            RegisterViewModel model = new RegisterViewModel();
            model.AppUserList = new List<RegisterModel>();

            using HttpResponseMessage httpResponse = await _apiHelper.ApiClient.GetAsync($"{ApiDictionary.UserApiUserList}/{username}");
            if (httpResponse != null && httpResponse.IsSuccessStatusCode)
            {
                users = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<List<RegisterModel>>>();
            }
            model.AppUserList = users.Data;
            return View(model);
        }
        #endregion

        [HttpGet]
        [Route("GetUserFromHRMS/{email}")]
        public async Task<ActionResult<RegisterModel>> GetOrgDetailsForProject(string email)
        {
            RegisterModel clnt = new RegisterModel();
            try
            {
                using HttpResponseMessage httpResponse = await _apiHelper.ApiClient.GetAsync($"{ApiDictionary.ClientApiGetClient}/{email}/EMAIL");
                if (httpResponse != null && httpResponse.IsSuccessStatusCode)
                {
                    clnt = await httpResponse.Content.ReadFromJsonAsync<RegisterModel>();
                }

            }
            catch (Exception ex)
            {

            }
            if (_httpContextAccessor.HttpContext.Request.IsAjaxRequest())
            {
                return Json(clnt);
            }
            return clnt;
        }


        #region Login
        public IActionResult Login()
        {

            try
            {
                LoginViewModel model = new LoginViewModel
                {
                    UserLoginModel = new LoginUserInfo()
                    {
                        LoginMode = _configuration.GetAppConfig("LoginMode")
                    }
                };
                return View("Login", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login");
                return BadRequest(ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                try
                {
                    if (ModelState.IsValid)
                    {

                        using HttpResponseMessage httpResponse = await _apiHelper.ApiClient.PostAsJsonAsync(ApiDictionary.UserApiAuthenticateUser, model.UserLoginModel);
                        if (httpResponse.IsSuccessStatusCode)
                        {

                            ApiResponse<LoginResponse> apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>();
                            if (apiResponse?.Data != null && apiResponse?.Status == 1)
                            {

                                _session.UserId(apiResponse.Data.UserId);
                                _session.UserName(apiResponse.Data.UserName);
                                _session.UserAuthToken(apiResponse.Data.AuthToken);
                                _session.EmployeeFullName(apiResponse.Data.EmployeeName);
                                if (string.IsNullOrEmpty(apiResponse.Data.RedirectUrl))
                                {
                                    return Redirect("/user/dashboard/index");
                                }
                                return Redirect(apiResponse.Data.RedirectUrl);
                            }
                            else
                            {
                                AppNotification.ShowMessage(this, apiResponse.Message.Text, MessageType.Error);
                            }
                        }
                        else
                        {
                            return Redirect($"Error/{(int)httpResponse.StatusCode}");

                            //AppNotification.ShowMessage(this, _sharedLocalizer["FailedToProcess"], MessageType.Error);
                        }
                    }
                    else
                    {
                        AppNotification.ShowMessage(this, _sharedLocalizer["ValidationErrorTitle"], "", MessageType.Error);
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "");
                    return BadRequest(ex);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login");
                return BadRequest(ex);
            }
            return View(model);//
        }


        [HttpGet]
        public async Task<ActionResult> ConfirmEmail(string token)
        {
            string userid = "";
            string reqtoken = "";
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return Redirect($"Error/1001");
                }
                if (!_protector.Unprotect(token).Split('&')[0].StartsWith("userid="))
                {
                    return Redirect($"Error/1001");
                }
                if (!_protector.Unprotect(token).Split('&')[1].StartsWith("token="))
                {
                    return Redirect($"Error/1001");
                }
                userid = _protector.Unprotect(token).Split('&')[0].Split("userid=")[1].ToString();
                reqtoken = _protector.Unprotect(token).Split('&')[1].Split("token=")[1].ToString();

                ApiResponse<string> apiResponse = new ApiResponse<string>();

                //EmailHelper objEmailHelper = new EmailHelper(_configuration, _elogger);
                using HttpResponseMessage httpResponse = await _apiHelper.ApiClient.GetAsync($"{ApiDictionary.UserApiConfirmEmail}?userId={userid ?? ""}&token={HttpUtility.UrlEncode(reqtoken) ?? ""}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (apiResponse?.Status == 1)
                    {
                        AppNotification.ShowMessage(this, apiResponse.StatusMessage, MessageType.Success);
                        return Redirect("/account/login");
                    }
                    else
                    {
                        return Redirect($"/Error/1001");
                    }
                }
                else
                {
                    return Redirect($"/Error/{(int)httpResponse.StatusCode}");
                }

            }
            catch (Exception ex)
            {
                return Redirect($"Error/404");
            }
        }
        #endregion

        #region Private Methods
        private async Task<bool> SendPasswordResetMail(string pEmailTo, string pUserName, string pNewPassword)
        {
            try
            {
                string strFilePath = "/EmailTemplate/PasswordResetTemplate.txt";
                string FolderPath = Path.Combine(_webHostEnvironment.WebRootPath.TrimEnd(new char[] { '\\' }), strFilePath.TrimStart(new char[] { '/' }));
                string pHtml = System.IO.File.ReadAllText(FolderPath);
                string pNewHtml = pHtml?.Replace("#USER_NAME#", pUserName).Replace("#NEW_PASSWORD#", pNewPassword);
                EmailHelper objEmailHelper = new EmailHelper(_configuration, _elogger);
                EmailModel objEmailModel = new EmailModel()
                {
                    EmailTo = pEmailTo,
                    EmailBcc = "",
                    EmailCc = "",
                    EmailPriority = MailPriority.High,
                    EmailSubject = "RESET PASSWORD",
                    EmailBody = pNewHtml,
                    IsEmailBodyHtml = true
                };
                return await objEmailHelper.SendEmail(objEmailModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendPasswordResetMail");
                return false;
            }
        }


        #endregion

        #region Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            using HttpResponseMessage httpResponse = await _apiHelper.ApiClient.GetAsync(ApiDictionary.UserApiLogout);
            if (httpResponse.IsSuccessStatusCode)
            {
                ApiResponse<string> apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<string>>();
                if (apiResponse.Data.ToUpper() == "LOGOUT SUCCESSFULL")
                {
                    _session.Clear();
                    AppNotification.ShowMessage(this, apiResponse.Data, MessageType.Success);
                    return Redirect("/");
                }
                else
                {
                    AppNotification.ShowMessage(this, "Log out is not successfull", MessageType.Error);
                    return Redirect("/Master/ProjectDetail/ProjectDetailsList");
                }

            }
            else
            {
                AppNotification.ShowMessage(this, "Log out is not successfull", MessageType.Error);
                return Redirect("/Master/ProjectDetail/ProjectDetailsList");
            }

        }

        #endregion

        #region Forget Password and Reset Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using HttpResponseMessage httpResponse = await _apiHelper.ApiClient.PostAsJsonAsync(ApiDictionary.UserApiForgetPassword, new ForgetPassord { Email = model.EmailId });
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        ApiResponse<string> apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<string>>();
                        if (apiResponse.Status == 1)
                        {
                            EmailHelper emailHelper = new EmailHelper(_configuration, _elogger);

                            string _EmailBody = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "EmailTemplate/PasswordResetEmailTempalte.txt"));
                            var sublink = _protector.Protect("Email=" + model.EmailId + "&Token=" + apiResponse.Data);
                            var link = Url.Action("ResetPassword", "Account", new { token = sublink }, Request.Scheme);
                            _EmailBody = _EmailBody.Replace("_PasswaordRestLink", link);

                            bool isMailSent = await emailHelper.SendEmail(new EmailModel { EmailTo = model.EmailId, EmailBody = _EmailBody, EmailSubject = "DeepShi Forget Password", EmailBcc = "", EmailCc = "", IsEmailBodyHtml = true });
                            //if (!isMailSent)
                            //{
                            //    throw Exception("Email Not send!!");
                            //}
                            AppNotification.ShowMessage(this, apiResponse.Message.Text, MessageType.Success);

                            //return Redirect($"/Account/Login");
                        }
                        else
                        {
                            AppNotification.ShowMessage(this, apiResponse.Message.Text, MessageType.Error);
                        }
                    }
                    else
                    {
                        return Redirect($"Error/{(int)httpResponse.StatusCode}");

                    }
                }
                else
                {
                    AppNotification.ShowMessage(this, _sharedLocalizer["ValidationErrorTitle"], "", MessageType.Error);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return BadRequest(ex);
            }
            return Redirect($"/Account/Login");//
        }

        [HttpGet]
        public async Task<ActionResult> ResetPassword(string token)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();

            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return Redirect($"Error/1001");
                }
                if (!_protector.Unprotect(token).Split('&')[0].StartsWith("Email="))
                {
                    return Redirect($"Error/1001");
                }
                if (!_protector.Unprotect(token).Split('&')[1].StartsWith("Token="))
                {
                    return Redirect($"Error/1001");
                }
                model.Email = _protector.Unprotect(token).Split('&')[0].Split("Email=")[1].ToString();
                model.Token = token;

            }
            catch (Exception ex)
            {
                return Redirect($"Error/404");
            }

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            try
            {
                if (_protector.Unprotect(model.Token).Split('&')[0].Split("Email=")[1].ToString().ToUpper() != model.Email.ToUpper())
                {
                    ModelState.AddModelError("", "Email id or token mismatch");
                }

                if (ModelState.IsValid)
                {
                    //model.Token = _protector.Unprotect(model.Token).Split('&')[1].Split("Token=")[1].ToString();

                    using HttpResponseMessage httpResponse = await _apiHelper.ApiClient.PostAsJsonAsync(ApiDictionary.UserApiResetPassword, new PasswordReset { Email = model.Email, Password = model.Password, Token = _protector.Unprotect(model.Token).Split('&')[1].Split("Token=")[1].ToString() });
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        ApiResponse<string> apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<string>>();
                        if (apiResponse.Status == 1)
                        {
                            AppNotification.ShowMessage(this, apiResponse.Message.Text, MessageType.Success);
                            return Redirect($"/Account/Login");
                        }
                        else
                        {
                            AppNotification.ShowMessage(this, apiResponse.Message.Text, MessageType.Error);
                            return View(model);
                        }

                    }
                    else
                    {
                        return Redirect($"Error/{(int)httpResponse.StatusCode}");

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Some error occur. Kindly Try aging");
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                return Redirect($"Error/404");
            }

            return View(model);
        }
        #endregion

        #region Change Password
        [HttpGet]
        public async Task<ActionResult> ChangePassword()
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.Email = _session.UserName();
            model.Token = "";

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ResetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //model.Token = _protector.Unprotect(model.Token).Split('&')[1].Split("Token=")[1].ToString();

                    using HttpResponseMessage httpResponse = await _apiHelper.ApiClient.PostAsJsonAsync(ApiDictionary.UserApiChangePassword, new PasswordReset { Email = model.Email, Password = model.Password, OldPassword = model.OldPassword });
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        ApiResponse<string> apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<string>>();
                        if (apiResponse.Status == 1)
                        {
                            AppNotification.ShowMessage(this, apiResponse.Message.Text, MessageType.Success);
                            return Redirect($"/Account/Logout");
                        }
                        else
                        {
                            AppNotification.ShowMessage(this, apiResponse.Message.Text, MessageType.Error);
                            return View(model);
                        }

                    }
                    else
                    {
                        return Redirect($"Error/{(int)httpResponse.StatusCode}");

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Some error occur. Kindly Try aging");
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                return Redirect($"Error/404");
            }

            return View(model);
        }
        #endregion

    }
}
