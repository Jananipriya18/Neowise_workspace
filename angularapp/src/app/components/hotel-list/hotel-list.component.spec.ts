import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HotelService } from '../../services/hotel.service';

import { HotelListComponent } from './hotel-list.component';

describe('HotelListComponent', () => {
  let component: HotelListComponent;
  let fixture: ComponentFixture<HotelListComponent>;
  let hotelService: HotelService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HotelListComponent],
      providers: [HotelService], // Provide the HotelService dependency
    });
    fixture = TestBed.createComponent(HotelListComponent);
    component = fixture.componentInstance;
    hotelService = TestBed.inject(HotelService); // Inject HotelService for testing
  });

  it('should create the hotel list component', () => {
    expect((component as any)).toBeTruthy();
  });

  it('should initialize hotels and filteredHotels on ngOnInit', () => {
    spyOn((hotelService as any), 'getAllHotels').and.returnValue([ {
      id: 1,
      name: 'Luxury Hotel',
      description: 'Luxury Hotel is a five-star hotel nestled in the heart of a bustling city. With its opulent décor, spacious rooms, and world-class amenities, it offers guests an unforgettable experience. Whether you are visiting for business or leisure, you will enjoy gourmet dining, a rooftop pool with panoramic city views, and impeccable service. Treat yourself to the epitome of luxury and sophistication at Luxury Hotel.',
      price: 300,
      availableRooms: 5,
      checkInDate: '2023-09-15',
      checkOutDate: '2023-09-20',
      imageSrc: 'assets/hotel1.jpg'
    },
    {
      id: 2,
      name: 'Another Hotel',
      description: 'Another Hotel is a cozy and charming place for your stay. Located in a serene environment, it offers comfortable rooms and a peaceful atmosphere. Whether you are on a family vacation or a solo trip, Another Hotel provides a warm and welcoming experience.',
      price: 150,
      availableRooms: 10,
      checkInDate: '2023-09-18',
      checkOutDate: '2023-09-22',
      imageSrc: 'assets/hotel2.jpg'
    }]);
    component['ngOnInit']();
    expect((component as any).hotels.length).toBe(2);
    expect((component as any).filteredHotels.length).toBe(2);
  });

  it('should filter hotels based on searchText when searchHotels is called', () => {
    component['hotels'] = [
      { name: 'Luxury Hotel' },
      { name: 'Beach Resort B' },
      { name: 'Another Hotel' },
    ];
    (component as any).searchText = 'hotel';
    (component as any).searchHotels();
    expect((component as any).filteredHotels.length).toBe(2);
  });

  it('should filter hotels case insensitively', () => {
    component['hotels'] = [
      { name: 'Luxury Hotel' },
      { name: 'Beach Resort B' },
      { name: 'Another Hotel' },
    ];
    (component as any).searchText = 'hotel';
    (component as any).searchHotels();
    expect((component as any).filteredHotels.length).toBe(2);
  });

  it('should not filter hotels when searchText is empty', () => {
    component['hotels'] = [
      { name: 'Hotel 1' },
      { name: 'Hotel 2' },
      { name: 'Another Hotel' },
    ];
    (component as any).searchText = '';
    (component as any).searchHotels();
    expect((component as any).filteredHotels.length).toBe(3);
  });

  it('should filter hotels correctly for partial matches', () => {
    component['hotels'] = [
      { name: 'Hotel 1' },
      { name: 'Hotel 2' },
      { name: 'Another Hotel' },
    ];
    (component as any).searchText = 'Hotel 1';
    (component as any).searchHotels();
    expect((component as any).filteredHotels.length).toBe(1);
    expect((component as any).filteredHotels[0].name).toBe('Hotel 1');
  });
});
