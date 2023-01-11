using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace DeepShiShared
{
    public enum AuthorizationType { Anonymous, Administrator, User };
    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ISession _session;
        readonly AuthorizationType _authorizationType;

        public AuthorizeActionFilter(AuthorizationType authorizationType, IHttpContextAccessor httpContextAccessor)
        {
            _authorizationType = authorizationType;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (_authorizationType != AuthorizationType.Anonymous)
            {
                if (context.RouteData.Values.TryGetValue("area", out object objArea))
                {
                    if (objArea != null && string.IsNullOrEmpty(_session.UserId()))
                    {
                        if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                        {
                            context.Result = new JsonResult("") { Value = new { Status = "Error", Message = "Authentication Failure" } };
                        }
                        else
                        {
                            string redirectUrl = $"{context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString}";
                            context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                                    { "Area", null },
                                    { "Controller", "Account" },
                                    { "Action", "Logout" },
                                    { "redirectUrl", redirectUrl }
                                });
                        }
                    }
                }
            }
        }
    }
    public sealed class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(AuthorizationType authorizationType) : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { authorizationType };
        }
    }

}
