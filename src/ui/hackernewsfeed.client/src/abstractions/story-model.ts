export interface Story {
  id: number;
  title: string;
  url?: string | null;
  score?: number;
  by?: string | null;
  dateTime?: Date;
  text?: string | null;
}

export class StoryModel implements Story {
  id: number;
  title: string;
  url?: string | null;
  score?: number;
  by?: string | null;
  dateTime?: Date;
  text?: string | null;

  constructor(data: Partial<Story> & { id: number; title: string }) {
    this.id = data.id;
    this.title = data.title;
    this.url = data.url ?? null;
    this.score = data.score ?? 0;
    this.by = data.by ?? null;
    this.dateTime = data.dateTime ?? new Date();
    this.text = data.text ?? null;
  }

  // Create a StoryModel from Hacker News API item
  static fromApi(item: any): StoryModel {
    return new StoryModel({
      id: Number(item.id),
      title: String(item.title ?? item.text ?? ''),
      url: item.url ?? null,
      score: item.score ?? item.points ?? 0,
      by: item.by ?? item.author ?? null,
      dateTime: item.dateTime ?? new Date(),
      text: item.text ?? null,
    });
  }

}
