import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { Stylist } from '../model/stylist.model'; // Adjust the import path as necessary
import { StylistListComponent } from './stylist-list.component';
import { StylistService } from '../services/stylist.service';

describe('StylistListComponent', () => {
  let component: StylistListComponent;
  let fixture: ComponentFixture<StylistListComponent>;
  let service: StylistService;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockStylists: Stylist[] = [
    {
      id: 1,
      name: 'Jane Doe',
      expertise: 'Fashion Consulting',
      styleSignature: 'Chic and Elegant',
      availability: 'Full-time',
      hourlyRate: 100,
      location: 'New York'
    }
  ];

  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [StylistListComponent],
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [StylistService, { provide: Router, useValue: spy }]
    });

    fixture = TestBed.createComponent(StylistListComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(StylistService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should create StylistListComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should call getStylists', () => {
    spyOn((service as any), 'getStylists').and.returnValue(of(mockStylists));
    (component as any).ngOnInit();
    expect((service as any).getStylists).toHaveBeenCalled();
    expect((component as any).stylists).toEqual(mockStylists);
  });

  fit('should call deleteStylist', () => {
    spyOn((service as any), 'deleteStylist').and.returnValue(of());
    (component as any).deleteStylist(1);
    expect((service as any).deleteStylist).toHaveBeenCalledWith(1);
  });

});
