# Hacker News Api Demo

## Summary

An Angular App with an ASP.NET Core backend that pulls the list of top stories (500) every hour from a StoryService pointing at Hacker News.

### Prerequisites

- .NET 9.0 SDK
- Angular CLI 19.2.19

## To Run This Project

1. Clone Locally
2. Browse to src/ui folder
3. Right-click and select 'HackerNewsFeed.Server' as Startup Project
4. Start Debugging (F5)

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

## To Upgrade Global Angular to Run This Project

```
npm uninstall -g @angular/cli
npm cache verify
npm install -g @angular/cli@latest@19.2.19
```