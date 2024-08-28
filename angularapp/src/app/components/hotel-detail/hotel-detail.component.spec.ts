import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { HotelService } from '../../services/hotel.service';
import { HotelDetailComponent } from './hotel-detail.component';

describe('HotelDetailComponent', () => {
  let component: HotelDetailComponent;
  let fixture: ComponentFixture<HotelDetailComponent>;
  let hotelService: HotelService;
  let activatedRoute: ActivatedRoute;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HotelDetailComponent],
      providers: [
        HotelService,
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: {
                get: () => 1, // Provide a sample hotel ID for testing
              },
            },
          },
        },
      ],
    });
    fixture = TestBed.createComponent(HotelDetailComponent);
    component = fixture.componentInstance;
    hotelService = TestBed.inject(HotelService);
    activatedRoute = TestBed.inject(ActivatedRoute);
  });

  it('should create the hotel detail component', () => {
    expect((component as any)).toBeTruthy();
  });

  it('should fetch hotel data on ngOnInit', () => {
    spyOn((hotelService as any), 'getHotelById').and.returnValue(    {
      id: 2,
      name: 'Beach Resort B',
      description: 'Beach Resort B is a tranquil paradise located on the pristine shores of a sun-kissed beach. This beachfront haven offers guests a peaceful escape from the everyday hustle and bustle. With its charming beachfront cottages, water sports activities, and a serene spa, Beach Resort B is the perfect destination for those seeking relaxation and adventure. Immerse yourself in the soothing sounds of the ocean and let Beach Resort B create cherished memories of sand, sea, and serenity.',
      price: 250,
      availableRooms: 3,
      checkInDate: '2023-09-18',
      checkOutDate: '2023-09-25',
      imageSrc: 'assets/hotel2.jpg'
    },);
    (component as any).ngOnInit();
    expect((component as any).hotel).toBeDefined();
    expect((component as any).hotel.name).toBe('Beach Resort B');
  });

  it('should book a hotel successfully', () => {
    spyOn(window, 'alert');
    (component as any).hotel = { id: 1, name: 'Test Hotel' };
    (component as any).bookHotel();
    expect(window.alert).toHaveBeenCalledWith('Hotel booked successfully: Test Hotel');
  });

  it('should make a reservation successfully when rooms are available', () => {
    spyOn((hotelService as any), 'checkAvailability').and.returnValue(true);
    spyOn((hotelService as any), 'reserveRooms');
    spyOn(window, 'alert');
    (component as any).hotel = { id: 1, name: 'Test Hotel' };
    (component as any).checkInDate = '2023-09-15';
    (component as any).checkOutDate = '2023-09-20';
    (component as any).makeReservation();
    expect(window.alert).toHaveBeenCalledWith('Reservation successful!');
    expect((hotelService as any).reserveRooms).toHaveBeenCalledWith(1, 1);
  });

  it('should show an alert when rooms are not available for reservation', () => {
    spyOn((hotelService as any), 'checkAvailability').and.returnValue(false);
    spyOn(window, 'alert');
    (component as any).hotel = { id: 1, name: 'Test Hotel' };
    (component as any).checkInDate = '2023-09-15';
    (component as any).checkOutDate = '2023-09-20';
    (component as any).makeReservation();
    expect(window.alert).toHaveBeenCalledWith('Rooms are not available for the selected dates.');
  });

  it('should handle an undefined hotel when fetching data on ngOnInit', () => {
    spyOn((hotelService as any), 'getHotelById').and.returnValue(undefined);
    spyOn(window, 'alert');
    (component as any).ngOnInit();
    expect((component as any).hotel).toBeUndefined();
  });

});
