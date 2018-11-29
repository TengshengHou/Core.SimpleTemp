using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Core.SimpleTemp.Mvc.Controllers
{
    [Authorize]
    [Route("Home")]
    [Route("")]
    public class HomeController : Controller
    {
        private const string README_CACHE_KEY = "README_CACHE_KEY";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDistributedCache _distributedCache;
        public HomeController(IHttpClientFactory httpClientFactory, IDistributedCache distributedCache)
        {
            _httpClientFactory = httpClientFactory;
            _distributedCache = distributedCache;
        }

        [HttpGet("index")]
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index");
        }


        [HttpGet("GetReadme")]
        public async Task<IActionResult> GetReadmeAsync()
        {
            string readmeHtml = "GitHub ReadMe抓取失败";
            if ((await _distributedCache.GetAsync(README_CACHE_KEY)) == null)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://github.com/TengshengHou/Core.SimpleTemp/blob/master/README.md");
                request.Headers.Add("Accept", "application/vnd.github.v3+json");
                request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
                var client = _httpClientFactory.CreateClient();
                var response = await client.SendAsync(request);
                var responseStr = await response.Content.ReadAsStringAsync();
                string expression = "#我是抓取开始标记#(.*?)#我是抓取标记结束#";
                var m = Regex.Match(responseStr, expression, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                readmeHtml = m?.Value;
                await _distributedCache.SetStringAsync(README_CACHE_KEY, readmeHtml, new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(5) });
            }
            else
            {
                readmeHtml = await _distributedCache.GetStringAsync(README_CACHE_KEY);
            }
            return Json(new { Result = "Success", Data = readmeHtml });
        }
    }
}
