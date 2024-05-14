
Using this solution should be fairly frictionless.

On initial start, Entity Framework code will create the database (using localdb instance of SQL Server), the single table needed and will populate it with default data.
Check appsettings.json > ConnectionStrings > DefaultConnection if this does not happen.

An initial Welcome / Home page is shown with a quick link to create a new short URL.

Clicking the "Shorten URL" menu option will show a summary of the URLs already held in the database. Viewing the details of any of these URLs will provide the short URL link.

Entering (or clicking) a short URL at any point will immediately perform a 302 (temporary) redirect to the long URL.
