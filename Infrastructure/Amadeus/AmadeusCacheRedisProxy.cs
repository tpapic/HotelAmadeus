using System;
using System.Text;
using System.Threading.Tasks;
using Application.Hotel;
using Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Amadeus
{
    public class AmadeusCacheRedisProxy : IAmadeusProxy
    {
        private readonly IAmadeus _amadeus;
        private readonly IDistributedCache _cache;
        public AmadeusCacheRedisProxy(IAmadeus amadeus, IDistributedCache cache)
        {
            _cache = cache;
            _amadeus = amadeus;
        }
        public async Task<Response> Search(Search.Query query)
        {
            var cacheKey = query.ToString();
            var redisAmadeus = await _cache.GetAsync(cacheKey);
            string serializedAmadeus = "";
            var amadeusData = new Response();

            if (redisAmadeus != null) {
                serializedAmadeus = Encoding.UTF8.GetString(redisAmadeus);
                amadeusData = JsonConvert.DeserializeObject<Response>(serializedAmadeus);
            } else {
                amadeusData = await _amadeus.Search(query);
                serializedAmadeus  = JsonConvert.SerializeObject(amadeusData);
                var encoded = Encoding.UTF8.GetBytes(serializedAmadeus);

                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                await _cache.SetAsync(cacheKey, encoded, options);
            }

            return amadeusData;
        }
    }
}