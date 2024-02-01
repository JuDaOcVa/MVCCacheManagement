using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Prj_JDOV_IDistributedCacheMVCApp.Models;
using System.Diagnostics;

namespace Prj_JDOV_IDistributedCacheMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCache _cache;

        public HomeController(ILogger<HomeController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CacheGet()
        {
            var tmp = _cache.GetString("CacheKey1");
            var cacheEntry = tmp == null ? new DateTime(1991, 1, 1) : DateTime.Parse(tmp);
            return View("Cache", cacheEntry);
        }

        public IActionResult CacheSet()
        {
            var cacheEntry = DateTime.Now;
            _cache.SetString("CacheKey1", cacheEntry.ToString());

            return View("Cache", cacheEntry);
        }

        public IActionResult CacheRemove()
        {
            var cacheEntry = new DateTime(1990, 1, 1);
            _cache.Remove("CacheKey1");

            return View("Cache", cacheEntry);
        }
    }
}