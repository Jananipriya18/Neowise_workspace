import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComicService } from './comic.service';

describe('ComicService', () => {
  let service: ComicService;
  let httpTestingController: HttpTestingController;

  const mockComics = [
    {
      id: 1,
      title: 'Spider-Man: No Way Home',
      author: 'Nick Spencer',
      series: 'Spider-Man',
      publisher: 'Marvel Comics',
      genre: 'Superhero',
      description: 'Peter Parker faces new challenges as Spider-Man in this thrilling continuation of the Spider-Man series.'
    },
    {
      id: 2,
      title: 'Batman: The Long Halloween',
      author: 'Jeph Loeb',
      series: 'Batman',
      publisher: 'DC Comics',
      publicationDate: '1996-10-01',
      genre: 'Superhero',
      description: 'A gripping detective story featuring Batman as he tries to uncover the identity of the mysterious Holiday killer.'
    },
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ComicService],
    });
    service = TestBed.inject(ComicService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    // Ensure that there are no outstanding requests after each test
    httpTestingController.verify();
  });

  fit('should_create_service_comic', () => {
    expect(service).toBeTruthy();
  });

  fit('should_retrieve_comics_from_the_API_via_GET', () => {
    (service as any).getComics().subscribe((comics) => {
      expect(comics).toEqual(mockComics);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}`);
    expect(req.request.method).toEqual('GET');
    req.flush(mockComics);
  });

  fit('should_add_a_comic_via_POST', () => {
    const newComic = {
        title: 'Spider-Man: No Way Home',
        author: 'Nick Spencer',
        series: 'Spider-Man',
        publisher: 'Marvel Comics',
        genre: 'Superhero',
        description: 'Peter Parker faces new challenges as Spider-Man in this thrilling continuation of the Spider-Man series.'
    };
    (service as any).addComic(newComic).subscribe((comic) => {
      expect(comic).toEqual(newComic);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}`);
    expect(req.request.method).toEqual('POST');
    req.flush(newComic);
  });


  fit('should_update_a_comic_via_PUT', () => {
    const updatedComic = {
      id: 1,
      title: 'Spider-Man: No Way Home',
      author: 'Nick Spencer',
      series: 'Spider-Man',
      publisher: 'Marvel Comics',
      genre: 'Superhero',
      description: 'Peter Parker faces new challenges as Spider-Man in this thrilling continuation of the Spider-Man series.'
    };
    (service as any).updateComic(updatedComic.id, updatedComic).subscribe((comic) => {
      expect(comic).toEqual(updatedComic);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}/${updatedComic.id}`);
    expect(req.request.method).toEqual('PUT');
    req.flush(updatedComic);
  });

});
