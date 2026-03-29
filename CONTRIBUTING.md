# Contributing

Contributions to this project are [released](https://help.github.com/articles/github-terms-of-service/#6-contributions-under-repository-license) to the public under the [project's open source license](LICENSE).

Everyone is welcome to contribute to this project. Contributing doesn't just mean submitting pull requests—there are many different ways for you to get involved, including answering questions, reporting issues, improving documentation, or suggesting new features.

## How to Contribute

### Reporting Issues

If you find a bug or have a feature request:
1. Check if the issue already exists in the [GitHub Issues](https://github.com/orassayag/cv-spider-v1-google-api/issues)
2. If not, create a new issue with:
   - Clear title and description
   - Steps to reproduce (for bugs)
   - Expected vs actual behavior
   - Error messages (if applicable)
   - Your environment details (OS, .NET version, SQL Server version)

### Submitting Pull Requests

1. Fork the repository
2. Create a new branch for your feature/fix:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Make your changes following the code style guidelines below
4. Test your changes thoroughly
5. Commit with clear, descriptive messages
6. Push to your fork and submit a pull request

### Code Style Guidelines

This project uses:
- **ASP.NET Web Forms** (.NET Framework 3.5)
- **C#** for server-side code
- **LINQ-to-SQL** for database operations
- **SQL Server** for data persistence

Before submitting:
```bash
# Build the project in Visual Studio
# Ensure no compilation errors
# Test the web pages manually
```

### Coding Standards

1. **Error handling**: Use try-catch blocks appropriately
2. **Database operations**: Use LINQ-to-SQL for all database interactions
3. **Code organization**: Keep code-behind logic in .cs files
4. **Naming**: Use clear, descriptive names for variables and methods
5. **Threading**: Be cautious with Thread.Sleep and concurrent operations
6. **Security**: Never hardcode credentials or sensitive data

### Adding New Features

When adding new features:
1. Update the database schema if needed (App_Code/MistikaDB.dbml)
2. Add appropriate error handling
3. Consider rate limiting for web scraping operations
4. Test thoroughly with different search terms
5. Update documentation

### Database Schema Changes

When modifying the database:
1. Update the LINQ-to-SQL model in `App_Code/MistikaDB.dbml`
2. Regenerate the designer code
3. Test all database operations
4. Document schema changes

## Security Considerations

When contributing, please be aware of:
1. **Google Terms of Service**: Automated scraping may violate Google's TOS
2. **Rate limiting**: Implement appropriate delays between requests
3. **Data privacy**: Handle email addresses responsibly and comply with anti-spam laws
4. **SQL injection**: Use parameterized queries (LINQ-to-SQL handles this)
5. **Connection strings**: Never commit actual database credentials

## Historical Context

This project was built in January 2012 as a proof-of-concept for automated lead generation. It uses deprecated Google search methods and should be considered a historical artifact. Modern implementations should use:
- Official Google APIs with proper authentication
- Respect robots.txt and website terms of service
- Implement proper GDPR/CCPA compliance
- Use modern web scraping frameworks

## Questions or Need Help?

Please feel free to contact me with any question, comment, pull-request, issue, or any other thing you have in mind.

* Or Assayag <orassayag@gmail.com>
* GitHub: https://github.com/orassayag
* StackOverflow: https://stackoverflow.com/users/4442606/or-assayag?tab=profile
* LinkedIn: https://linkedin.com/in/orassayag

Thank you for contributing! 🙏
