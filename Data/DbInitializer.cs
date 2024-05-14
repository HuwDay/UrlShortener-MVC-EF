
using UrlShortener.Models;
using System;
using System.Linq;

namespace UrlShortener.Data
{
    public class DbInitializer
    {
        public static void Initialize(UrlContext context)
        { 
            context.Database.EnsureCreated();

            if(context.ShortUrls.Any())
            {
                return;
            }

            var shortUrls = new ShortUrl[]
            {
                new ShortUrl{Url = "https://news.bbc.co.uk/"}
            };
            foreach( ShortUrl shortUrl in shortUrls )
            {
                context.ShortUrls.Add( shortUrl );
            }
            context.SaveChanges();
        }

    }
}
