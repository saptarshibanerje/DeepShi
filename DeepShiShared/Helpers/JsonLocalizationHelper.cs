using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace DeepShiShared
{

    public static class JsonLocalizationHelper
    {
        public static void RefreshLocalizationCache(Type type, string resourcePath, params string[] cultures)
        {
            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            IMemoryCache memoryCache = httpContextAccessor?.HttpContext.RequestServices.GetService<IMemoryCache>();
            IHostEnvironment hostEnvironment = httpContextAccessor?.HttpContext.RequestServices.GetService<IHostEnvironment>();

            foreach (var culture in cultures)
            {
                string resourceNameWithCulture = $"{type?.Name}.{culture}";
                string resourceFilePath = Path.Combine(hostEnvironment?.ContentRootPath, resourcePath, $"{resourceNameWithCulture}.json");

                if (File.Exists(resourceFilePath))
                {
                    string jsonData = File.ReadAllText(resourceFilePath, Encoding.UTF8);
                    ConcurrentDictionary<string, string> resourceData = JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(jsonData);
                    memoryCache.Set(resourceNameWithCulture, resourceData, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(2) });
                }
            }
        }
    }

    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly string _resourceFilePath;
        private readonly string _resourceNameWithCulture;
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, string> _resourceData;

        public JsonStringLocalizer(string resourcePath, string resourceName, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _resourceNameWithCulture = $"{resourceName}.{CultureInfo.CurrentUICulture}";
            _resourceFilePath = Path.Combine(resourcePath, $"{_resourceNameWithCulture}.json");
            _memoryCache.TryGetValue(_resourceNameWithCulture, out _resourceData);
            if (_resourceData == null)
            {
                if (File.Exists(_resourceFilePath))
                {
                    string jsonData = File.ReadAllText(_resourceFilePath, Encoding.UTF8);
                    _resourceData = JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(jsonData);
                    _memoryCache.Set(_resourceNameWithCulture, _resourceData, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(2) });
                }
            }
        }

        public LocalizedString this[string name]
        {
            get
            {
                if (_resourceData != null && _resourceData.TryGetValue(name, out string value))
                {
                    return new LocalizedString(name, value ?? name);
                }

                return new LocalizedString(name, name);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public static class JsonLocalizationServiceExtension
    {
        public static IServiceCollection AddJsonLocalization(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }
            services.AddOptions();

            return services;
        }

        public static IServiceCollection AddJsonLocalization(this IServiceCollection services, Action<LocalizationOptions> action)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }
            if (action == null)
            {
                throw new ArgumentException(nameof(action));
            }
            AddJsonLocalizationServices(services);
            services.Configure(action);
            return services;
        }

        internal static void AddJsonLocalizationServices(IServiceCollection services)
        {
            services.TryAddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.TryAddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
            services.TryAddTransient(typeof(IStringLocalizer), typeof(StringLocalizer));
        }
    }

    internal class StringLocalizer : IStringLocalizer
    {
        private IStringLocalizer _localizer;

        public StringLocalizer(IHostEnvironment env, IStringLocalizerFactory factory)
        {
            _localizer = factory.Create(string.Empty, env.ContentRootPath);
        }

        public LocalizedString this[string name] => _localizer[name];

        public LocalizedString this[string name, params object[] arguments] => _localizer[name, arguments];

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _localizer.GetAllStrings(includeParentCultures);

        public IStringLocalizer WithCulture(CultureInfo culture) => _localizer.WithCulture(culture);
    }

    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly string _resourcePath;
        private readonly IMemoryCache _memoryCache;
        public JsonStringLocalizerFactory(IOptions<LocalizationOptions> localizationOptions, IHostEnvironment hostEnvironment, IMemoryCache memoryCache)
        {
            if (localizationOptions == null)
            {
                throw new ArgumentNullException(nameof(localizationOptions));
            }
            _resourcePath = Path.Combine(hostEnvironment.ContentRootPath, (localizationOptions.Value?.ResourcesPath ?? string.Empty));
            _memoryCache = memoryCache;
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException();
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            if (resourceSource == null)
            {
                throw new ArgumentException(nameof(resourceSource));
            }
            return new JsonStringLocalizer(_resourcePath, resourceSource.Name, _memoryCache);
        }

    }

}
