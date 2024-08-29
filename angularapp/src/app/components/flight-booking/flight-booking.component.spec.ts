import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { FlightService } from '../../services/flight.service';
import { FlightBookingComponent } from './flight-booking.component';

describe('FlightBookingComponent', () => {
  let component: FlightBookingComponent;
  let fixture: ComponentFixture<FlightBookingComponent>;
  let flightService: FlightService;
  let activatedRoute: ActivatedRoute;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FlightBookingComponent],
      providers: [
        FlightService,
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: {
                get: () => 1, // Provide a sample flight ID for testing
              },
            },
          },
        },
      ],
    });
    fixture = TestBed.createComponent(FlightBookingComponent);
    component = fixture.componentInstance;
    flightService = TestBed.inject(FlightService);
    activatedRoute = TestBed.inject(ActivatedRoute);
  });

  it('should create the flight detail component', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch flight data on ngOnInit', () => {
    spyOn(flightService, 'getFlightById').and.returnValue({
      id: 2,
      name: 'Flight 202',
      airline: 'Airways XYZ',
      description: 'Flight 202 is a comfortable journey from City A to City B with excellent onboard services.',
      price: 350,
      availableSeats: 20,
      departureDate: '2023-10-01',
      arrivalDate: '2023-10-02',
      imageSrc: 'assets/flight2.jpg'
    });

    component.ngOnInit();
    expect(component.flight).toBeDefined();
    expect(component.flight.name).toBe('Flight 202');
    expect(component.flight.airline).toBe('Airways XYZ');
  });

  it('should book a flight successfully', () => {
    spyOn(window, 'alert');
    component.flight = { id: 1, name: 'Flight 101' };
    component.bookFlight();
    expect(window.alert).toHaveBeenCalledWith('Flight booked successfully: Flight 101');
  });

  it('should make a reservation successfully when seats are available', () => {
    spyOn(flightService, 'checkAvailability').and.returnValue(true);
    spyOn(flightService, 'reserveSeats');
    spyOn(window, 'alert');
    component.flight = { id: 1, name: 'Flight 101' };
    component.departureDate = '2023-09-15';
    component.arrivalDate = '2023-09-20';
    component.makeReservation();
    expect(window.alert).toHaveBeenCalledWith('Reservation successful!');
    expect(flightService.reserveSeats).toHaveBeenCalledWith(1, 1); // Assuming reserving one seat
  });

  it('should show an alert when seats are not available for reservation', () => {
    spyOn(flightService, 'checkAvailability').and.returnValue(false);
    spyOn(window, 'alert');
    component.flight = { id: 1, name: 'Flight 101' };
    component.departureDate = '2023-09-15';
    component.arrivalDate = '2023-09-20';
    component.makeReservation();
    expect(window.alert).toHaveBeenCalledWith('Seats are not available for the selected dates.');
  });

  it('should handle an undefined flight when fetching data on ngOnInit', () => {
    spyOn(flightService, 'getFlightById').and.returnValue(undefined);
    spyOn(window, 'alert');
    component.ngOnInit();
    expect(component.flight).toBeUndefined();
    expect(window.alert).toHaveBeenCalledWith('Flight not found.');
  });

});
