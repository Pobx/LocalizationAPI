using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LocalizationAPI
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly IDistributedCache _distributedCache;
        private Dictionary<string, string> localizationData;

        public JsonStringLocalizer(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;

            localizationData = new Dictionary<string, string>();
            localizationData.Add("HELLO_TH", "สวัสดีเด้อพี่น้อง");
            localizationData.Add("HELLO_EN", "Hello");
        }
        public LocalizedString this[string name]
        {
            get
            {
                var value = GetLocalizedString(name);
                return new LocalizedString(name, value ?? name, value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var theActualValue = this[name];

                return !theActualValue.ResourceNotFound ? new LocalizedString(name, string.Format(theActualValue.Value, arguments), false) : theActualValue;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return localizationData.Select(item => new LocalizedString(item.Key, item.Value, false));
        }

        private string GetLocalizedString(string key)
        {
            var localize = Thread.CurrentThread.CurrentCulture.Name.Split("-")[0];
            string cacheKey = $"{key}_{localize}".ToUpper();
            string cacheValue = _distributedCache.GetString(cacheKey);
            string result;

            result = localizationData.GetValueOrDefault(cacheKey);

            //if (string.IsNullOrEmpty(cacheValue))
            //{
            //    result = localizationData.GetValueOrDefault(cacheKey);
            //    _distributedCache.SetString(cacheKey, result);
            //}
            //else
            //{
            //    result = cacheValue;
            //}

            return result;
        }
    }
}
