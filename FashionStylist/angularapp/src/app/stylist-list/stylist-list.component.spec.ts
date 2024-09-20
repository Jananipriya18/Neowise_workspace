import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { Stylist } from '../model/stylist.model'; // Adjust the import path as necessary
import { StylistListComponent } from './stylist-list.component';
import { StylistService } from '../services/stylist.service';
import { By } from '@angular/platform-browser';

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

  fit('should_create_StylistListComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_call_getStylists', () => {
    spyOn((service as any), 'getStylists').and.returnValue(of(mockStylists));
    (component as any).ngOnInit();
    expect((service as any).getStylists).toHaveBeenCalled();
    expect((component as any).stylists).toEqual(mockStylists);
  });

  fit('should_call_deleteStylist', () => {
    spyOn((service as any), 'deleteStylist').and.returnValue(of());
    (component as any).deleteStylist(1);
    expect((service as any).deleteStylist).toHaveBeenCalledWith(1);
  });

  // Test to check for the presence of the buttons
  fit('should_check_if_the_filter_buttons_are_present', () => {
    spyOn(service, 'getStylists').and.returnValue(of(mockStylists));
    fixture.detectChanges(); // Trigger initial data binding

    const basicButton = fixture.debugElement.query(By.css('.filter-basic'));
    const premiumButton = fixture.debugElement.query(By.css('.filter-premium'));
    const luxuryButton = fixture.debugElement.query(By.css('.filter-luxury'));

    expect(basicButton).toBeTruthy();
    expect(premiumButton).toBeTruthy();
    expect(luxuryButton).toBeTruthy();
  });

  fit('should_filter_stylists_based_on_basic_premium_and_luxury packages', () => {
    spyOn(service, 'getStylists').and.returnValue(of(mockStylists));
    component.ngOnInit(); // Call ngOnInit to load stylists
  
    // Test for basic package (Hourly rate < 50)
    component.filterStylists('basic');
    const basicStylists = mockStylists.filter(stylist => stylist.hourlyRate < 50);
    expect(component.filteredStylists.length).toBe(basicStylists.length); // Dynamically check the count
    basicStylists.forEach(stylist => {
      expect(stylist.hourlyRate).toBeLessThan(50);
    });
  
    // Test for premium package (50 <= Hourly rate <= 100)
    component.filterStylists('premium');
    const premiumStylists = mockStylists.filter(stylist => stylist.hourlyRate >= 50 && stylist.hourlyRate <= 100);
    expect(component.filteredStylists.length).toBe(premiumStylists.length); // Dynamically check the count
    premiumStylists.forEach(stylist => {
      expect(stylist.hourlyRate).toBeGreaterThanOrEqual(50);
      expect(stylist.hourlyRate).toBeLessThanOrEqual(100);
    });
  
    // Test for luxury package (Hourly rate > 100)
    component.filterStylists('luxury');
    const luxuryStylists = mockStylists.filter(stylist => stylist.hourlyRate > 100);
    expect(component.filteredStylists.length).toBe(luxuryStylists.length); // Dynamically check the count
    luxuryStylists.forEach(stylist => {
      expect(stylist.hourlyRate).toBeGreaterThan(100);
    });
  });
  

});
