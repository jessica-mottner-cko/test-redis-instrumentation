using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestRedisInstrumentationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public TestController(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
        }

        /// <summary>
        /// https://localhost:5001/api/test/test1
        /// 15000 iterations
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("test1")]
        public IActionResult Test5(CancellationToken ct = default)
        {
            var conn = _redisConnection.GetDatabase();
            var autoGenKey = Guid.NewGuid().ToString();

            conn.StringSet(new RedisKey(autoGenKey), new RedisValue("helloworld"), TimeSpan.FromMinutes(5));

            return new OkObjectResult($"Add Redis Entry {autoGenKey} (sync)");
        }

        /// <summary>
        /// https://localhost:5001/api/test/test2
        /// 15000 iterations
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("test2")]
        public async Task<IActionResult> Test6(CancellationToken ct = default)
        {
            var conn = _redisConnection.GetDatabase();
            var autoGenKey = Guid.NewGuid().ToString();

            await conn.StringSetAsync(new RedisKey(autoGenKey), new RedisValue("helloworld"), TimeSpan.FromMinutes(5));

            return new OkObjectResult($"Add Redis Entry {autoGenKey} (async)");
        }
    }
}
