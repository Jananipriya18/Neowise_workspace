  export interface Comic {
    id?: number;                   // Unique identifier for the comic
    title: string;                // Title of the comic book
    author: string;               // Author(s) of the comic book
    series: string;               // Series name to which the comic belongs
    publisher: string;            // Publisher of the comic book
    publicationDate: Date;        // Date when the comic book was published
    genre: string;                // Genre or category of the comic book
    description?: string;         // Optional brief description or synopsis
  }
  