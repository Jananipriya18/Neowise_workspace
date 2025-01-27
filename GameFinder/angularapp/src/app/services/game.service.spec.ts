import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { GameService } from './game.service';

describe('GameService', () => {
  let service: GameService;
  let httpTestingController: HttpTestingController;

  const mockGames = [
    {
      id: 1,
      title: 'The Witcher 3: Wild Hunt',
      releaseYear: 2015,
      genre: 'Action RPG',
      developer: 'CD Projekt Red',
      supportContact: 'support@cdprojektred.com'
    },
    {
      id: 2,
      title: 'Red Dead Redemption 2',
      releaseYear: 2018,
      genre: 'Action-Adventure',
      developer: 'Rockstar Games',
      supportContact: 'support@rockstargames.com'
    },
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [GameService],
    });
    service = TestBed.inject(GameService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    // Ensure that there are no outstanding requests after each test
    httpTestingController.verify();
  });

  fit('should_create_service_game', () => {
    expect(service).toBeTruthy();
  });

  fit('should_retrieve_games_from_the_API_via_GET', () => {
    (service as any).getGames().subscribe((games) => {
      expect(games).toEqual(mockGames);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}`);
    expect(req.request.method).toEqual('GET');
    req.flush(mockGames);
  });

  fit('should_add_a_game_via_POST', () => {
    const newGame = {
      title: 'The Witcher 3: Wild Hunt',
      releaseYear: 2015,
      genre: 'Action RPG',
      developer: 'CD Projekt Red',
      supportContact: 'support@cdprojektred.com'
    };
    (service as any).addGame(newGame).subscribe((game) => {
      expect(game).toEqual(newGame);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}`);
    expect(req.request.method).toEqual('POST');
    req.flush(newGame);
  });


  fit('should_update_a_game_via_PUT', () => {
    const updatedGame = {
      id: 1,
      title: 'The Witcher 3: Wild Hunt',
      releaseYear: 2015,
      genre: 'Action RPG',
      developer: 'CD Projekt Red',
      supportContact: 'support@cdprojektred.com'
    };
    (service as any).updateGame(updatedGame.id, updatedGame).subscribe((game) => {
      expect(game).toEqual(updatedGame);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}/${updatedGame.id}`);
    expect(req.request.method).toEqual('PUT');
    req.flush(updatedGame);
  });

});
