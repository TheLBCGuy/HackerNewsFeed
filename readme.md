## Summary

An Angular App with an ASP.NET Core backend that pulls the list of top stories (500) every hour from a StoryService pointing at Hacker News.

## To Upgrade Global Angular to Run This Project

```
npm uninstall -g @angular/cli
npm cache verify
npm install -g @angular/cli@latest@19.2.19
```

## Todo

add angular unit tests

move vetted on service side instead of front-end

clean-up, refactor

## Later Todos

burst story retrieval

client keeps current list (whether load fresh or latest search), only requests new stories from server on load/refresh

improve storyservice cacheing (with app settings)