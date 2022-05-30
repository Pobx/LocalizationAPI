using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;

namespace LocalizationAPI
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IDistributedCache _distributedCache;
        public JsonStringLocalizerFactory(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocalizer(_distributedCache);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonStringLocalizer(_distributedCache);
        }
    }
}
