using UrlShortener.Models;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Data
{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions<UrlContext> options) : base(options)
        {
        }

        public DbSet<ShortUrl> ShortUrls { get; set; }

    }
}
