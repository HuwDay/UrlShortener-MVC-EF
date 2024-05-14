using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class ShortUrlsController : Controller
    {
        private readonly UrlContext _context;
        private Regex urlValidationRegex = new Regex("^(http(s):\\/\\/.)[-a-zA-Z0-9@:%._\\+~#=]{2,256}\\.[a-z]{2,6}\\b([-a-zA-Z0-9@:%_\\+.~#?&\\/=]*)$");

        public ShortUrlsController(UrlContext context)
        {
            _context = context;
        }

        // GET: ShortUrls
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShortUrls.ToListAsync());
        }

        // GET: ShortUrls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shortUrl = await _context.ShortUrls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shortUrl == null)
            {
                return NotFound();
            }

            return View(shortUrl);
        }

        // GET: ShortUrls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShortUrls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Url")] ShortUrl shortUrl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shortUrl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shortUrl);
        }

        // GET: ShortUrls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shortUrl = await _context.ShortUrls.FindAsync(id);
            if (shortUrl == null)
            {
                return NotFound();
            }
            return View(shortUrl);
        }

        // POST: ShortUrls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Url")] ShortUrl shortUrl)
        {
            if (id != shortUrl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shortUrl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShortUrlExists(shortUrl.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shortUrl);
        }

        // GET: ShortUrls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shortUrl = await _context.ShortUrls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shortUrl == null)
            {
                return NotFound();
            }

            return View(shortUrl);
        }

        // POST: ShortUrls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shortUrl = await _context.ShortUrls.FindAsync(id);
            if (shortUrl != null)
            {
                _context.ShortUrls.Remove(shortUrl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShortUrlExists(int id)
        {
            return _context.ShortUrls.Any(e => e.Id == id);
        }

    }
}
