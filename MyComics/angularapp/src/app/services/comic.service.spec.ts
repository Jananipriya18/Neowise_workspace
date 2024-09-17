import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComicService } from './comic.service';

describe('ComicService', () => {
  let service: ComicService;
  let httpTestingController: HttpTestingController;

  const mockComics = [
    {
      id: 1,
      name: 'Dr. John Smith',
      age: 45,
      specialization: 'Cardiology',
      department: 'Cardiology',
      contactNumber: '1234567890'
    },
    {
      id: 2,
      name: 'Dr. Jane Doe',
      age: 38,
      specialization: 'Neurology',
      department: 'Neurology',
      contactNumber: '9876543210'
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
        publicationDate: '2021-09-01',
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
      publicationDate: '2021-09-01',
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



  fit('should_delete_a_comic_via_DELETE', () => {
    const comicId = 1;
    (service as any).deleteComic(comicId).subscribe(() => {
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}/${comicId}`);
    expect(req.request.method).toEqual('DELETE');
    req.flush({});
  });
});
