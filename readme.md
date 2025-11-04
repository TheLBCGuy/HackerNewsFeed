## Summary

An Angular App with an ASP.NET Core backend that pulls the list of top stories (500) every hour from a StoryService pointing at Hacker News.

## To Upgrade Global Angular to Run This Project

```
npm uninstall -g @angular/cli
npm cache verify
npm install -g @angular/cli@latest@19.2.19
```

## Todo

burst story retrieval

make items ui pretty

improve storyservice cacheing (with app settings)

add angular unit tests

clean-up, refactor

