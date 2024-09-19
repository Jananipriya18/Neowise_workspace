import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ComicListComponent } from './comic-list.component';
import { ComicService } from '../services/comic.service';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { Comic } from '../model/comic.model';

describe('ComicListComponent', () => {
  let component: ComicListComponent;
  let fixture: ComponentFixture<ComicListComponent>;
  let service: ComicService;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockComics: Comic[] = [
    {
      id: 1,
      title: 'Spider-Man: Homecoming',
      author: 'Stan Lee',
      series: 'Spider-Man',
      publisher: 'Marvel',
      genre: 'Superhero',
      description: 'A young superhero with spider-like abilities.'
    }
  ];

  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [ComicListComponent],
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [ComicService, { provide: Router, useValue: spy }]
    });

    fixture = TestBed.createComponent(ComicListComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(ComicService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should_create_ComicListComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_call_getComics', () => {
    spyOn(service, 'getComics').and.returnValue(of(mockComics));
    component.ngOnInit();
    expect(service.getComics).toHaveBeenCalled();
    expect(component.comics).toEqual(mockComics);
  });

});
