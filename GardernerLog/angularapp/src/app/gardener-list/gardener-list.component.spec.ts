import { ComponentFixture, TestBed } from '@angular/core/testing';
import { GardenerListComponent } from './gardener-list.component'; // Update component import
import { GardenerService } from '../services/gardener.service'; // Update service import
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { Gardener } from '../model/gardener.model'; // Update model import

describe('GardenerListComponent', () => { // Update class name
  let component: GardenerListComponent; // Update component type
  let fixture: ComponentFixture<GardenerListComponent>; // Update fixture type
  let service: GardenerService; // Update service type
  let routerSpy: jasmine.SpyObj<Router>;

  const mockGardeners: Gardener[] = [ // Update mock data
    { id: 1, name: 'John Doe', age: 30, task: 'Planting', description: 'Planting new flowers', durationInMinutes: 60 }
  ];

  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [GardenerListComponent], // Update component declaration
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [GardenerService, { provide: Router, useValue: spy }] // Update service
    });

    fixture = TestBed.createComponent(GardenerListComponent); // Update component instantiation
    component = fixture.componentInstance; // Update component instance
    service = TestBed.inject(GardenerService); // Update service instance
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should_create_GardenerListComponent', () => { // Update test name
    expect(component).toBeTruthy();
  });

  fit('should_call_getGardeners', () => { // Update test name
    spyOn(service, 'getGardeners').and.returnValue(of(mockGardeners)); // Update method
    component.ngOnInit(); // Update method call
    expect(service.getGardeners).toHaveBeenCalled(); // Update method call
    expect(component.gardeners).toEqual(mockGardeners); // Update property
  });

  fit('should_call_deleteGardener', () => { // Update test name
    spyOn(service, 'deleteGardener').and.returnValue(of()); // Update method
    component.deleteGardener(1); // Update method call
    expect(service.deleteGardener).toHaveBeenCalledWith(1); // Update method call
  });

  fit('should_calculate_weekly_duration_correctly', () => {
    const dailyDuration = 60; // Duration in minutes
    const expectedWeeklyDuration = (dailyDuration * 7) / 60; // 7 days, converting to hours
    const result = component.calculateWeeklyDuration(dailyDuration);
    expect(result).toEqual(expectedWeeklyDuration);
  });

  fit('should_calculate_weekly_duration', () => {
    const dailyDuration = 60; // Duration in minutes
    const expectedWeeklyDuration = (dailyDuration * 7) / 60; // 7 days, converting to hours
    const result = component.calculateWeeklyDuration(dailyDuration); // Call the method to test
    expect(result).toEqual(expectedWeeklyDuration); // Check if the calculated result matches the expected value
  });

});
