import { StoryModel } from './abstractions';

let story1: StoryModel = {
  id: 1,
  title: 'Title 1',
  url: 'www.story1.com'
};

let story2: StoryModel = {
  id: 2,
  title: 'Title 2',
  url: 'www.story2.com'
};

export const mockResult = {
  stories: [
    story1,
    story2
  ],
  total: 2
};
