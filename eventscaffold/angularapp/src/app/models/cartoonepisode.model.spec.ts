import { CartoonEpisode } from './cartoon-episode.model';

describe('CartoonEpisode', () => {
  it('should_create_episode_instance', () => {
    const episode: CartoonEpisode = {
      episodeId: 1,
      cartoonSeriesName: 'Test Cartoon Series Name',
      episodeTitle: 'Test Episode Title',
      releaseDate: 'Test Release Date',
      directorName: 'Test Director Name',
      duration: 'Test Duration',
      description: 'Test Description'
    };

    // Check if the episode object exists
    expect(episode).toBeTruthy();

    // Check individual properties of the episode
    expect(episode.episodeId).toBe(1);
    expect(episode.cartoonSeriesName).toBe('Test Cartoon Series Name');
    expect(episode.episodeTitle).toBe('Test Episode Title');
    expect(episode.releaseDate).toBe('Test Release Date');
    expect(episode.directorName).toBe('Test Director Name');
    expect(episode.duration).toBe('Test Duration');
    expect(episode.description).toBe('Test Description');
  });
});
