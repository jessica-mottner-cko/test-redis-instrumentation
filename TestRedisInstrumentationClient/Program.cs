using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace TestRedisInstrumentationClient
{
    class Program
    {
        private static int _nbIterations = 100;

        public static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                int.TryParse(args[0], out _nbIterations);
            }
            
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var task = Task.Run(() =>
            {
                var j = 0;
                while (j++ <= _nbIterations)
                {
                    try
                    {
                        var request = $"https://localhost:5001/api/test/test1".GetStringAsync().GetAwaiter().GetResult();
                        Console.WriteLine($"Response: {request}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            });
            await Task.WhenAll(task);
        }
    }
}
