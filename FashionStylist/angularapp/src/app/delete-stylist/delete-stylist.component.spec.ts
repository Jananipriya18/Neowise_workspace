import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DeleteStylistComponent } from './delete-stylist.component';
import { StylistService } from '../services/stylist.service'; // assuming the service location
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { Router } from '@angular/router';
import { Stylist } from '../model/stylist.model'; // assuming the model location
import { jasmine } from 'jasmine-core';

describe('DeleteStylistComponent', () => {
  let component: DeleteStylistComponent;
  let fixture: ComponentFixture<DeleteStylistComponent>;
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

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('Router', ['navigate']); // Create a spy for the Router service

    await TestBed.configureTestingModule({
      declarations: [DeleteStylistComponent],
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [StylistService, { provide: Router, useValue: spy }]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteStylistComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(StylistService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should create DeleteStylistComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should call deleteStylist', () => {
    spyOn(service, 'deleteStylist').and.returnValue(of()); // Spy on the deleteStylist method
    component.deleteStylist(1); // Call the deleteStylist method
    expect(service.deleteStylist).toHaveBeenCalledWith(1); // Check if deleteStylist was called with the correct ID
  });
});
