using UrlShortener.Data;
using UrlShortener.Models;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace UrlShortener.Controllers
{
    public class UrlLookupController : Controller
    {
        private readonly UrlContext _context;

        public UrlLookupController(UrlContext context) {
            _context = context;
        }

        public IActionResult Index(string urlKey)
        {
            int urlId = ShortUrl.UrlKeyToId(urlKey);
            if (urlId == -1) {
                return Redirect("/");
            }

            var shortUrl = _context.ShortUrls.ToList().FirstOrDefault(su => su.Id == urlId);
            if (shortUrl == null)
            {
                return Redirect("/");
            }
            else
            {
                return Redirect(shortUrl.Url);
            }
        }

    }
}
