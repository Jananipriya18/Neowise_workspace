import { ComponentFixture, TestBed } from '@angular/core/testing';
import { GameListComponent } from './game-list.component';
import { GameService } from '../services/game.service';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { Game } from '../model/game.model';

describe('GameListComponent', () => {
  let component: GameListComponent;
  let fixture: ComponentFixture<GameListComponent>;
  let service: GameService;
  let routerSpy: jasmine.SpyObj<Router>;

  // Mock data
  const mockGames: Game[] = [
    {
      id: 1,
      title: 'The Legend of Code',
      releaseYear: 2020,
      genre: 'Adventure',
      developer: 'CodeMasters',
      supportContact: 'support@codemasters.com',
    },
    {
      id: 2,
      title: 'Angular Warriors',
      releaseYear: 2019,
      genre: 'Action',
      developer: 'Frontend Heroes',
      supportContact: 'support@frontendheroes.io',
    },
  ];

  beforeEach(() => {
    // Spy object for Router
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    // Configure the testing module
    TestBed.configureTestingModule({
      declarations: [GameListComponent],
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [GameService, { provide: Router, useValue: spy }],
    });

    // Create the component and inject dependencies
    fixture = TestBed.createComponent(GameListComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(GameService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should create the GameListComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should call getGames on initialization', () => {
    // Spy on the getGames method and mock its return value
    spyOn(service, 'getGames').and.returnValue(of(mockGames));

    // Call ngOnInit to trigger the service call
    component.ngOnInit();

    // Assert that getGames was called and games array is set
    expect(service.getGames).toHaveBeenCalled();
    expect(component.games).toEqual(mockGames);
  });

  fit('should call searchByDeveloper and set developerGameCount correctly', () => {
    // Spy on getGamesByDeveloper and mock its return value
    spyOn(service, 'getGamesByDeveloper').and.returnValue(of([mockGames[1]]));

    // Set the developer name and call searchByDeveloper
    component.developerName = 'CodeMasters';
    component.searchByDeveloper();

    // Assert that getGamesByDeveloper was called
    expect(service.getGamesByDeveloper).toHaveBeenCalledWith('CodeMasters');
    
    // Assert that the games and developerGameCount were updated
    expect(component.games.length).toBe(1);
    expect(component.developerGameCount).toBe(1);
  });
});
