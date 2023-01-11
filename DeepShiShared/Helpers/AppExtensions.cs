using DeepShiShared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeepShiShared
{
    public static class ConfigurationExtensions
    {
        public static string GetAppConfig(this IConfiguration configuration, string key)
        {
            return configuration.GetSection("AppConfig")[key];
        }
    }

    public static class SessionExtensions
    {
        /// <summary>
        /// To set User Name into Session Data
        /// </summary>
        /// <param name="value"></param>
        public static void UserName(this ISession session, string value)
        {
            session.Set<string>("USER_NAME", value);
        }

        /// <summary>
        /// To get User Name from Session Data
        /// </summary>
        /// <returns></returns>
        public static string UserName(this ISession session)
        {
            return session.Get<string>("USER_NAME");
        }

        /// <summary>
        /// To set User ID into Session Data
        /// </summary>
        /// <param name="value"></param>
        public static void UserId(this ISession session, string value)
        {
            session.Set<string>("USER_ID", value);
        }

        /// <summary>
        /// To get User ID from Session Data
        /// </summary>
        /// <returns></returns>
        public static string UserId(this ISession session)
        {
            return session.Get<string>("USER_ID");
        }

        /// <summary>
        /// To set User Type into Session Data
        /// </summary>
        /// <param name="value"></param>
        public static void UserType(this ISession session, string value)
        {
            session.Set<string>("USER_TYPE", value);
        }

        /// <summary>
        /// To get User Type from Session Data
        /// </summary>
        /// <returns></returns>
        public static string UserType(this ISession session)
        {
            return session.Get<string>("USER_TYPE");
        }

        /// <summary>
        /// To set User Type into Session Data
        /// </summary>
        /// <param name="value"></param>
        public static void UserCompany(this ISession session, string value)
        {
            session.Set<string>("USER_COMP", value);
        }

        /// <summary>
        /// To get User Type from Session Data
        /// </summary>
        /// <returns></returns>
        public static string UserCompany(this ISession session)
        {
            return session.Get<string>("USER_COMP");
        }

        /// <summary>
        /// To set User Division into Session Data
        /// </summary>
        /// <param name="value"></param>
        public static void UserDivision(this ISession session, string value)
        {
            session.Set<string>("USER_DIVN", value);
        }

        /// <summary>
        /// To get User Division from Session Data
        /// </summary>
        /// <returns></returns>
        public static string UserDivision(this ISession session)
        {
            return session.Get<string>("USER_DIVN");
        }

        /// <summary>
        /// To set User Department into Session Data
        /// </summary>
        /// <param name="value"></param>
        public static void UserDepartment(this ISession session, string value)
        {
            session.Set<string>("USER_DEPT", value);
        }

        /// <summary>
        /// To get User Department from Session Data
        /// </summary>
        /// <returns></returns>
        public static string UserDepartment(this ISession session)
        {
            return session.Get<string>("USER_DEPT");
        }

        /// <summary>
        /// To set EmployeeFullName into Session Data
        /// </summary>
        /// <param name="value"></param>
        public static void EmployeeFullName(this ISession session, string value)
        {
            session.Set<string>("EMPLOYEE_FULL_NAME", value);
        }

        /// <summary>
        /// To get User Name from Session Data
        /// </summary>
        /// <returns></returns>
        public static string EmployeeFullName(this ISession session)
        {
            return session.Get<string>("EMPLOYEE_FULL_NAME");
        }



        public static void UserAuthToken(this ISession session, string value)
        {
            session.Set<string>("USER_AUTH_TOKEN", value);
        }


        public static string UserAuthToken(this ISession session)
        {
            return session.Get<string>("USER_AUTH_TOKEN");
        }

        /// <summary>
        /// To set User Division into Session Data
        /// </summary>
        /// <param name="value"></param>
        public static void UserLanguage(this ISession session, string value)
        {
            session.Set<string>("USER_LANG", value);
        }

        /// <summary>
        /// To get User Division from Session Data
        /// </summary>
        /// <returns></returns>
        public static string UserLanguage(this ISession session)
        {
            return String.IsNullOrEmpty(session.Get<string>("USER_LANG")) ? "ENG" : session.Get<string>("USER_LANG");
        }

        /// <summary>
        /// To set User Division into Session Data
        /// </summary>
        /// <param name="value"></param>
        public static void UserProfilePicture(this ISession session, string value)
        {
            session.Set<string>("USER_PROFILE_PICTURE", value);
        }

        /// <summary>
        /// To get User Division from Session Data
        /// </summary>
        /// <returns></returns>
        public static string UserProfilePicture(this ISession session)
        {
            return session.Get<string>("USER_PROFILE_PICTURE");
        }

        public static string SessionId(this ISession session)
        {
            return session.Id;
        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }

        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

    public static class HttpRequestExtensions
    {
        private const string RequestedWithHeader = "X-Requested-With";
        private const string XmlHttpRequest = "XMLHttpRequest";

        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.Headers != null)
            {
                return request.Headers[RequestedWithHeader] == XmlHttpRequest;
            }

            return false;
        }
    }

    public static class HelperExtension
    {
        public static string NullToString(this string str)
        {
            return str == null ? "" : str;
        }
    }
}
