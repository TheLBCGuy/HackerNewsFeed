# Hacker News Api Demo

## Summary

An Angular App with an ASP.NET Core backend that pulls and displays the list of top stories (500) every hour from Hacker News with a HostedService.

Hacker News API information can be found here [Github Hacker News Project](https://github.com/HackerNews/API)

Story titles, author and text (if available, first 100 characters) are displayed in a paginated grid.

Story titles, author and text (if available) are indexed in memory using Lucene for quick search and retrieval.

### Prerequisites

- .NET 9.0 SDK
- Angular CLI 19.2.19

## To Run This Project

1. Clone Locally
2. Open in Visual Studio (or other preferred IDE)
3. Browse to src/ui folder
4. Right-click and select 'HackerNewsFeed.Server' as Startup Project
5. Start Debugging (F5)

### Unit Testing

This project contains the following type of tests:

- Unit Tests with xUnit for ASP.NET Core Backend
- Angular Unit Tests with Karma and Jasmine
- Integration Tests

To Run Angular Unit Tests:

1. Navigate to the src/ui/hackernewsfeed.client folder via the Command Prompt
2. Run the following command:
```
ng test
```

## To Run ASP.NET Core Unit Tests:

1. Browse to the src/tests folder in the Solution Explorer
2. Right-click on the HackerNewsFeed.Server.Tests project
3. Select 'Run Tests'

# Other Information

## To Upgrade your Global Angular to Run This Project

```
npm uninstall -g @angular/cli
npm cache verify
npm install -g @angular/cli@latest@19.2.19
```

## Help with GitHub Actions

# Install Act to Help with Testing Github Actions Locally

## See this site for more information:

https://nektosact.com/introduction.html

## See this site for Winget installation instructions:

https://nektosact.com/installation/winget.html

**NOTE** Act's local Docker Container has an issue when running the 'ng test' when a **ChromeHeadless** browser is required, however the angular workflow in this project works fine within Github itself