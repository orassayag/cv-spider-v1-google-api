# Instructions

## Setup Instructions

### Prerequisites

- Windows operating system
- Microsoft .NET Framework 3.5 or higher
- Microsoft SQL Server 2008 or higher (Express edition is sufficient)
- Visual Studio 2010 or higher (or Visual Studio Community Edition)
- Internet connection for web scraping operations

### Database Setup

1. Open SQL Server Management Studio
2. Create a new database named `MistikaDB`
3. Execute the following SQL script to create the required table:

```sql
CREATE DATABASE MistikaDB;
GO

USE MistikaDB;
GO

CREATE TABLE [dbo].[CVMail] (
    [MailID] BIGINT NOT NULL PRIMARY KEY,
    [MailValue] NVARCHAR(MAX) NULL,
    [MailSent] BIT NULL,
    [MailDate] DATETIME NULL
);
GO
```

4. Update the connection string in `web.config`:
   - Replace `Data Source=OR-PC\SQLEXPRESS` with your SQL Server instance name
   - Ensure the `Initial Catalog=MistikaDB` matches your database name
   - Adjust authentication settings as needed

### Project Setup

1. Open the project in Visual Studio
2. Verify NuGet packages are installed:
   - Newtonsoft.Json (JSON parsing library)
3. Build the project:
   - Press `Ctrl+Shift+B` or go to Build > Build Solution
4. Resolve any compilation errors

### Configuration

#### Connection String (web.config)

```xml
<connectionStrings>
  <add name="MistikaDBConnectionString" 
       connectionString="Data Source=YOUR_SERVER\SQLEXPRESS;Initial Catalog=MistikaDB;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

#### Search Terms (Default3.aspx.cs)

Edit the `lista` array in the `Page_Load` method to customize search terms:

```csharp
string[] lista = new string[]
{
    "your search term 1",
    "your search term 2",
    // Add more search terms here
};
```

## Running the Application

### Main Scraper (Default3.aspx)

1. Navigate to `Default3.aspx` in your browser
2. The application will:
   - Iterate through all search terms in the `lista` array
   - Query Google search for each term
   - Extract URLs from search results
   - Visit each URL and scrape for email addresses
   - Store unique email addresses in the database
   - Wait 25 seconds between requests (rate limiting)

### Google API Test Page (DefaultGoogleApi.aspx)

1. Navigate to `DefaultGoogleApi.aspx` in your browser
2. This page demonstrates the Google Search API interface
3. You can test different search queries

### Email Writer (DefaultWriter.aspx)

This page was designed for sending emails to collected addresses (implementation varies).

## How It Works

### Search Process Flow

```
1. Page loads → Iterate through search terms
2. For each search term:
   ├─ Query Google search (10 results per page)
   ├─ Parse HTML response
   ├─ Extract URLs from search results
   ├─ Visit each URL
   ├─ Scrape page content
   ├─ Extract email addresses using regex
   ├─ Filter invalid emails (images, duplicates)
   ├─ Store unique emails in database
   └─ Wait 25 seconds (rate limiting)
3. Repeat for next search term
```

### Email Extraction Regex

The application uses this regex pattern to find email addresses:

```regex
[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?
```

### Database Operations

The application:
- Checks if email already exists before inserting
- Auto-increments MailID for new records
- Marks emails as not sent by default (`MailSent = false`)
- Records timestamp when email is found (`MailDate`)

## Important Notes

### Rate Limiting

The application includes deliberate delays:
- 25-second wait between search queries
- This prevents overwhelming Google's servers
- Reduces risk of IP blocking

### Google Search Limitations

⚠️ **Important**: This application uses screen scraping of Google search results, which:
- May violate Google's Terms of Service
- Can result in IP blocking or CAPTCHA challenges
- Is unreliable as Google frequently changes their HTML structure
- Should be replaced with official Google Custom Search API

### Data Privacy and Legal Compliance

⚠️ **Legal Warning**: When collecting and storing email addresses:
- Comply with GDPR, CCPA, CAN-SPAM Act, and local regulations
- Obtain proper consent before sending marketing emails
- Provide clear opt-out mechanisms
- Maintain accurate records of consent
- This tool should only be used for legitimate business purposes

### Performance Considerations

- The application is single-threaded and synchronous
- Processing 100 search terms could take 40+ minutes
- Consider running during off-peak hours
- Monitor database size and implement cleanup routines

## Troubleshooting

### "503 Service Unavailable" Errors

If you see 503 errors:
- Google has rate-limited your IP
- Increase wait time between requests
- Use a VPN or proxy
- Consider using official Google APIs instead

### Database Connection Errors

If you cannot connect to SQL Server:
- Verify SQL Server service is running
- Check connection string in web.config
- Ensure database exists
- Verify user permissions

### No Emails Found

If the scraper isn't finding emails:
- Google may be blocking automated access
- Websites may have changed their structure
- Email addresses may be obfuscated (JavaScript-rendered)
- Check regex pattern is still accurate

### LINQ-to-SQL Errors

If you encounter LINQ errors:
- Rebuild the LINQ-to-SQL model (MistikaDB.dbml)
- Verify database schema matches the model
- Check connection string

## Development

### Modifying Search Logic

To change search behavior, edit `Default3.aspx.cs`:
- `GetPosition()`: Main search entry point
- `FindPosition()`: URL extraction and scraping logic
- `getPageSource()`: HTML fetching

### Adding New Data Fields

To capture additional information:
1. Add columns to CVMail table in SQL Server
2. Update `App_Code/MistikaDB.dbml` model
3. Modify insertion logic in `FindPosition()` method

### Testing Changes

1. Use a small set of search terms for testing
2. Monitor database to verify data is being saved
3. Check application logs for errors
4. Test rate limiting behavior

## Modern Alternatives

For new projects, consider:
- **Google Custom Search JSON API**: Official API with proper authentication
- **Scrapy**: Modern Python web scraping framework
- **Puppeteer/Playwright**: Headless browser automation
- **Email verification services**: Validate emails before storing
- **CRM integration**: Direct integration with customer relationship management systems

## Author

* **Or Assayag** - *Initial work* - [orassayag](https://github.com/orassayag)
* Or Assayag <orassayag@gmail.com>
* GitHub: https://github.com/orassayag
* StackOverflow: https://stackoverflow.com/users/4442606/or-assayag?tab=profile
* LinkedIn: https://linkedin.com/in/orassayag
