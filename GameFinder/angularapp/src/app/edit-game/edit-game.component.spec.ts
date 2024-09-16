import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EditGameComponent } from './edit-game.component';
import { GameService } from '../services/game.service';
import { of, throwError } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Game } from '../model/game.model';

describe('EditGameComponent', () => {
  let component: EditGameComponent;
  let fixture: ComponentFixture<EditGameComponent>;
  let service: GameService;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockGame: Game = {
    id: 1,
    title: 'The Witcher 3: Wild Hunt',
    releaseYear: 2015,
    genre: 'Action RPG',
    developer: 'CD Projekt Red',
    supportContact: 'support@cdprojektred.com'
  };

  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [EditGameComponent],
      imports: [HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [
        GameService,
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => '1' } } } },
        { provide: Router, useValue: spy }
      ]
    });

    fixture = TestBed.createComponent(EditGameComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(GameService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should_create_EditGameComponent', () => {
    expect((component as any)).toBeTruthy();
  });

  fit('should_load_game_details_on_init', () => {
    spyOn((service as any), 'getGameById').and.returnValue(of(mockGame));
    (component as any).ngOnInit();
    fixture.detectChanges();

    expect((service as any).getGameById).toHaveBeenCalledWith(1);
    expect((component as any).game).toEqual(mockGame);
  });

  fit('should_save_game_details', () => {
    spyOn((service as any), 'updateGame').and.returnValue(of(mockGame));
    (component as any).game = { ...mockGame };
    (component as any).saveGame();
    
    expect((service as any).updateGame).toHaveBeenCalledWith(mockGame.id, mockGame);
  });

});
