export interface StoryModel {
  id: number;
  title: string;
  text: string;
  url: string;
  score?: number;
  by?: string | null;
  dateTime?: Date;
}

export interface Pagination<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}
