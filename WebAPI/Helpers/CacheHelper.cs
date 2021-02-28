using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class CacheHelper
    {
        public static string DirectoriesKey { get; set; }
        public static string RedisCacheKey { get; set; }

        public CacheHelper()
        {
            DirectoriesKey = Guid.NewGuid().ToString();
            RedisCacheKey = Guid.NewGuid().ToString();
        }
    }
}
