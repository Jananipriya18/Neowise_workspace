import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FlightService } from '../../services/flight.service';
import { FlightListComponent } from './flight-list.component';

describe('FlightListComponent', () => {
  let component: FlightListComponent;
  let fixture: ComponentFixture<FlightListComponent>;
  let flightService: FlightService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FlightListComponent],
      providers: [FlightService], // Provide the FlightService dependency
    });
    fixture = TestBed.createComponent(FlightListComponent);
    component = fixture.componentInstance;
    flightService = TestBed.inject(FlightService); // Inject FlightService for testing
  });

  it('should create the flight list component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize flights and filteredFlights on ngOnInit', () => {
    spyOn(flightService, 'getAllFlights').and.returnValue([{
      id: 1,
      name: 'Flight 101',
      airline: 'Airline A',
      description: 'Flight 101 is a comfortable journey from City A to City B with excellent onboard services.',
      price: 200,
      availableSeats: 15,
      departureDate: '2023-09-15',
      arrivalDate: '2023-09-16',
      imageSrc: 'assets/flight1.jpg'
    },
    {
      id: 2,
      name: 'Flight 202',
      airline: 'Airline B',
      description: 'Flight 202 is a quick trip from City C to City D with all the essentials covered.',
      price: 150,
      availableSeats: 10,
      departureDate: '2023-09-18',
      arrivalDate: '2023-09-18',
      imageSrc: 'assets/flight2.jpg'
    }]);
    component.ngOnInit();
    expect(component.flights.length).toBe(2);
    expect(component.filteredFlights.length).toBe(2);
  });

  it('should filter flights based on searchText when searchFlights is called', () => {
    component.flights = [
      { name: 'Flight 101', airline: 'Airline A' },
      { name: 'Flight 202', airline: 'Airline B' },
      { name: 'Flight 303', airline: 'Airline C' },
    ];
    component.searchText = 'Flight';
    component.searchFlights();
    expect(component.filteredFlights.length).toBe(3);
  });

  it('should filter flights case insensitively', () => {
    component.flights = [
      { name: 'Flight 101', airline: 'Airline A' },
      { name: 'Flight 202', airline: 'Airline B' },
      { name: 'Flight 303', airline: 'Airline C' },
    ];
    component.searchText = 'flight';
    component.searchFlights();
    expect(component.filteredFlights.length).toBe(3);
  });

  it('should not filter flights when searchText is empty', () => {
    component.flights = [
      { name: 'Flight 101', airline: 'Airline A' },
      { name: 'Flight 202', airline: 'Airline B' },
      { name: 'Flight 303', airline: 'Airline C' },
    ];
    component.searchText = '';
    component.searchFlights();
    expect(component.filteredFlights.length).toBe(3);
  });

  it('should filter flights correctly for partial matches', () => {
    component.flights = [
      { name: 'Flight 101', airline: 'Airline A' },
      { name: 'Flight 202', airline: 'Airline B' },
      { name: 'Flight 303', airline: 'Airline C' },
    ];
    component.searchText = '101';
    component.searchFlights();
    expect(component.filteredFlights.length).toBe(1);
    expect(component.filteredFlights[0].name).toBe('Flight 101');
  });
});
