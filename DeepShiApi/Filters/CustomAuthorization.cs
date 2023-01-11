using DeepShiApi.TokenRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeepShiApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {
        /// <summary>  
        /// This will Authorize User  
        /// </summary>  
        /// <returns></returns>  
        /// 
        private readonly string[] allowedroles;
        public CustomAuthorization(params string[] roles)
        {
            this.allowedroles = roles;
        }


        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (!SkipAuthorization(filterContext))
            {
                if (filterContext != null)
                {

                    var _token = filterContext.HttpContext.Request.Headers[HeaderNames.Authorization].ToString();
                    var _configSection = (IConfiguration)filterContext.HttpContext.RequestServices.GetService(typeof(IConfiguration));
                    var tokenService = (ITokenService)filterContext.HttpContext.RequestServices.GetService(typeof(ITokenService));

                    if (!string.IsNullOrEmpty(_token))
                    {
                        string authToken = _token;
                        if (authToken != null)
                        {
                            if (tokenService.IsTokenValid(_configSection["Jwt:Key"].ToString(), _configSection["Jwt:Issuer"].ToString(), authToken.Replace("Bearer ", "")))
                            {
                                //filterContext.HttpContext.Response.Headers.Add("Bearer", authToken);
                                //filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");

                                //filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");

                                return;
                            }
                            else
                            {
                                filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                                filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                                filterContext.Result = new JsonResult("NotAuthorized")
                                {
                                    Value = new
                                    {
                                        Status = "Error",
                                        Message = "Invalid Token"
                                    },
                                };
                            }

                        }

                    }
                    else
                    {
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide authToken";
                        filterContext.Result = new JsonResult("Please Provide authToken")
                        {
                            Value = new
                            {
                                Status = "Error",
                                Message = "Please Provide authToken"
                            },
                        };
                    }
                }
            }



        }

        public bool IsValidToken(string authToken)
        {

            return true;

        }

        private static bool SkipAuthorization(AuthorizationFilterContext context)
        {
            var filters = context.Filters;
            for (var i = 0; i < filters.Count; i++)
            {
                if (filters[i] is IAllowAnonymousFilter)
                {
                    return true;
                }
            }

            // When doing endpoint routing, MVC does not add AllowAnonymousFilters for AllowAnonymousAttributes that
            // were discovered on controllers and actions. To maintain compat with 2.x,
            // we'll check for the presence of IAllowAnonymous in endpoint metadata.
            var endpoint = context.HttpContext.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return true;
            }

            return false;
        }
    }
}
