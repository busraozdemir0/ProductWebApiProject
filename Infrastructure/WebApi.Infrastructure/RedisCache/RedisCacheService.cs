using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Interfaces.RedisCache;

namespace WebApi.Infrastructure.RedisCache
{
    // Redis => anahtar-deger ciftlerini depolamak icin kullanilan bir veritabanidir.
    //Redis, verileri hafizada saklar, bu da veri eisimini hizlandirir. 
    //Redis genellikle önbellek(cache) çözümleri, oturum yonetimi gibi islemlerde kullanilir.
    public class RedisCacheService : IRedisCacheService
    {
        private readonly ConnectionMultiplexer redisConnection; // Redis kutuphanesi yardimiyla gelen ozellik
        private readonly IDatabase database; // Redis kutuphanesi yardimiyla gelen ozellik
        private readonly RedisCacheSettings settings;
        public RedisCacheService(IOptions<RedisCacheSettings> options)
        {
            settings=options.Value;
            var opt = ConfigurationOptions.Parse(settings.ConnectionString);
            redisConnection = ConnectionMultiplexer.Connect(opt);
            database = redisConnection.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await database.StringGetAsync(key);
            if (value.HasValue)
                return JsonConvert.DeserializeObject<T>(value);

            return default;
        }

        public async Task SetAsync<T>(string key, T value, DateTime? expirationTime = null)
        {
            TimeSpan timeUnitExpiration = expirationTime.Value - DateTime.Now; // Bu sekilde TimeSpan turunde veri elde etmis oluyoruz (StringSetAsync metodu TimeSpan istedigi icin donusturduk.)
            await database.StringSetAsync(key,JsonConvert.SerializeObject(value), timeUnitExpiration);
        }
    }
}
