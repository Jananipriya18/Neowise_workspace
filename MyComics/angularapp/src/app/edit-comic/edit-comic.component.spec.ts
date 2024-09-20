import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { EditComicComponent } from './edit-comic.component';
import { ComicService } from '../services/comic.service';
import { of } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
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
    genre: 'Fantasy',
    description: 'An Elseworlds story where Superman lands in the Soviet Union.',
  };

  beforeEach(() => {
    routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);

    TestBed.configureTestingModule({
      declarations: [EditComicComponent],
      imports: [HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [
        ComicService,
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => '1' } } } },
        { provide: Router, useValue: routerSpy }
      ]
    });

    fixture = TestBed.createComponent(EditComicComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(ComicService);
  });

  fit('should_create_EditComicComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_load_comic_details_on_init', fakeAsync(() => {
    spyOn(service, 'getComicById').and.returnValue(of(mockComic));
    component.ngOnInit();
    tick(); // Ensure that all async operations are completed
    fixture.detectChanges();

    expect(service.getComicById).toHaveBeenCalledWith(1);
    expect(component.comic).toEqual(mockComic);
  }));

  fit('should_save_comic_details', fakeAsync(() => {
    spyOn(service, 'updateComic').and.returnValue(of(mockComic));
    component.comic = { ...mockComic };
    component.saveComic();
    tick(); // Ensure that all async operations are completed

    expect(service.updateComic).toHaveBeenCalledWith(mockComic.id, mockComic);
    expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('/comicsList');
  }));
});
