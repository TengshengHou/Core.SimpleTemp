using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
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
            //var request = new HttpRequestMessage(HttpMethod.Get, "https://github.com/TengshengHou/Core.SimpleTemp/blob/master/README.md");
            //request.Headers.Add("Accept", "application/vnd.github.v3+json");
            //request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            //var client = _httpClientFactory.CreateClient();
            //var response = await client.SendAsync(request);
            //var str = await response.Content.ReadAsStringAsync();
            
            return View("Index");
        }
    }
}
