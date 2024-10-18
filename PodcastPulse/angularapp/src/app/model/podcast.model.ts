export interface Podcast {
  id?: number;                    // Unique identifier for the podcast (optional)
  title: string;                  // Name of the podcast
  description: string;            // Summary of the podcast's content
  hostName: string;               // Name of the podcast host
  category: string;               // Podcast category/genre
  releaseDate: string;              // string the podcast was released
  contactEmail?: string;          // Optional contact email for the host
  episodeCount: number;           // Number of episodes available
}
