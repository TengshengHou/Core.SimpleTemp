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


namespace Core.SimpleTemp.Mvc.Controllers
{
    [Authorize]
    [Route("Home")]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("index")]
        [HttpGet("")]
        public async Task<IActionResult> IndexAsync()
        {
            ViewData["UserName"] = User.Identity.Name;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://github.com/TengshengHou/Core.SimpleTemp/blob/master/README.md");
                request.Headers.Add("Accept", "application/vnd.github.v3+json");
                request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
                var client = _httpClientFactory.CreateClient();
                var response = await client.SendAsync(request);
                var str = await response.Content.ReadAsStringAsync();

                string pattern12 = "#我是抓取开始标记#(.*?)#我是抓取标记结束#";

                var m = Regex.Match(str, pattern12, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                ViewData["ReadmeHtml"] = m?.Value ?? "GitHub ReadMe抓取失败";
            }
            catch (Exception)
            {

            }
            return View("Index");
        }
    }
}
