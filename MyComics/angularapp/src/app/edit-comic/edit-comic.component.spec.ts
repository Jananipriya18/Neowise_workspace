import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EditComicComponent } from './edit-comic.component';
import { ComicService } from '../services/comic.service';
import { of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Comic } from '../model/comic.model';

describe('EditComicComponent', () => {
  let component: EditComicComponent;
  let fixture: ComponentFixture<EditComicComponent>;
  let service: ComicService;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockComic: Comic = {
    id: 1,
    title: 'Superman: Red Son',
    author: 'Mark Millar',
    series: 'Elseworlds',
    publisher: 'DC Comics',
    publicationDate: new Date('2003-01-01'),
    genre: 'Alternate History',
    description: 'An Elseworlds story where Superman lands in the Soviet Union.',
  };

  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [EditComicComponent],
      imports: [HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [
        ComicService,
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => '1' } } } },
        { provide: Router, useValue: spy }
      ]
    });

    fixture = TestBed.createComponent(EditComicComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(ComicService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should create EditComicComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should load comic details on init', () => {
    spyOn(service, 'getComicById').and.returnValue(of(mockComic));
    component.ngOnInit();
    fixture.detectChanges();

    expect(service.getComicById).toHaveBeenCalledWith(1);
    expect(component.comic).toEqual(mockComic);
  });

  fit('should save comic details', () => {
    spyOn(service, 'updateComic').and.returnValue(of(mockComic));
    component.comic = { ...mockComic };
    component.saveComic();
    
    expect(service.updateComic).toHaveBeenCalledWith(mockComic.id, mockComic);
  });

});
