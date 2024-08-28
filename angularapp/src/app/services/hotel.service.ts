import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class HotelService {
  private hotels = [
    {
      id: 1,
      name: 'Luxury Hotel ',
      description: 'Luxury Hotel is a five-star hotel nestled in the heart of a bustling city. With its opulent décor, spacious rooms, and world-class amenities, it offers guests an unforgettable experience. Whether you are visiting for business or leisure, you will enjoy gourmet dining, a rooftop pool with panoramic city views, and impeccable service. Treat yourself to the epitome of luxury and sophistication at Luxury Hotel.',
      price: 300,
      availableRooms: 5,
      checkInDate: '2023-09-15',
      checkOutDate: '2023-09-20',
      imageSrc: 'assets/hotel1.jpg'
    },
    {
      id: 2,
      name: 'Beach Resort B',
      description: 'Beach Resort B is a tranquil paradise located on the pristine shores of a sun-kissed beach. This beachfront haven offers guests a peaceful escape from the everyday hustle and bustle. With its charming beachfront cottages, water sports activities, and a serene spa, Beach Resort B is the perfect destination for those seeking relaxation and adventure. Immerse yourself in the soothing sounds of the ocean and let Beach Resort B create cherished memories of sand, sea, and serenity.',
      price: 250,
      availableRooms: 3,
      checkInDate: '2023-09-18',
      checkOutDate: '2023-09-25',
      imageSrc: 'assets/hotel2.jpg'
    },
    {
      id: 3,
      name: 'City Center Hotel',
      description: 'City Center Hotel is a modern and stylish urban retreat situated in the heart of the bustling city center. Offering convenience and comfort to both business and leisure travelers, this hotel boasts contemporary design, well-appointed rooms, and easy access to the citys attractions. Guests can indulge in gourmet dining, unwind at the rooftop bar with panoramic city views, and explore the vibrant city life just steps away from the hotels  doorstep. City Center Hotel is your gateway to a dynamic city experience, where convenience meets sophistication.',
      price: 180,
      availableRooms: 4,
      checkInDate: '2023-09-24',
      checkOutDate: '2023-09-27',
      imageSrc: 'assets/hotel3.jpg'
    },
    {
      id: 4,
      name: 'Cozy Cottage',
      description: 'Cozy Cottage is a charming and rustic retreat nestled in the tranquil countryside. Surrounded by lush greenery and scenic landscapes, this quaint cottage offers guests a cozy and intimate getaway. The cottage features comfortable interiors with a fireplace, making it perfect for a romantic escape or a quiet weekend retreat. Guests can enjoy leisurely walks in the nearby woods, cozy evenings by the fire, and the simple pleasures of country living. Cozy Cottage is the ideal destination for those seeking peace, relaxation, and a touch of rustic charm.',
      price: 120,
      availableRooms: 6,
      checkInDate: '2023-09-19',
      checkOutDate: '2023-09-23',
      imageSrc: 'assets/hotel4.jpg'
    },
    {
      id: 5,
      name: 'Desert Mirage Resort',
      description: 'Desert Mirage Resort is a luxurious oasis nestled in the heart of the mesmerizing desert landscape. This resort offers a unique blend of modern comfort and desert adventure. Guests can unwind in well-appointed rooms and suites with stunning desert views, indulge in spa treatments inspired by local traditions, and embark on thrilling desert safaris and excursions. Whether you are seeking relaxation by the pool or the thrill of dune bashing, Desert Mirage Resort provides an unforgettable desert experience. Immerse yourself in the enchanting beauty of the desert and create lasting memories at this captivating resort.',
      price: 160,
      availableRooms: 12,
      checkInDate: '2023-09-25',
      checkOutDate: '2023-09-30',
      imageSrc: 'assets/hotel5.jpg'
    },
    {
      id: 6,
      name: 'Mountain View Lodge',
      description: 'Experience the serenity of the mountains at Mountain View Lodge. Nestled in a picturesque mountain range, this lodge offers cozy accommodations with breathtaking views of lush forests and towering peaks. Guests can explore hiking trails, enjoy bonfires under starry skies, and savor hearty mountain cuisine. Whether you are a nature enthusiast or simply seeking a peaceful retreat, Mountain View Lodge is the perfect destination for your mountain getaway.',
      price: 130,
      availableRooms: 8,
      checkInDate: '2023-09-18',
      checkOutDate: '2023-09-23',
      imageSrc: 'assets/hotel6.jpg'
    },
    {
      id: 7,
      name: 'Seaside Paradise Resort',
      description: 'Seaside Paradise Resort offers the ultimate beachfront escape. Located on a pristine tropical island, this resort boasts white sandy beaches, crystal-clear waters, and luxurious accommodations. Guests can relax in beachfront villas, indulge in spa treatments, and enjoy water sports and snorkeling adventures. Whether you are lounging on the beach or exploring the vibrant underwater world, Seaside Paradise Resort promises a slice of paradise.',
      price: 220,
      availableRooms: 10,
      checkInDate: '2023-09-20',
      checkOutDate: '2023-09-25',
      imageSrc: 'assets/hotel7.jpg'
    },
    {
      id: 8,
      name: 'Urban Retreat Hotel',
      description: 'Discover the charm of city living at Urban Retreat Hotel. Situated in the heart of a bustling metropolis, this hotel offers modern comfort with easy access to cultural attractions and entertainment. Guests can unwind in stylish rooms, dine at rooftop restaurants, and explore the citys vibrant nightlife. Whether you are a business traveler or a tourist, Urban Retreat Hotel provides an urban oasis for your stay.',
      price: 180,
      availableRooms: 15,
      checkInDate: '2023-09-22',
      checkOutDate: '2023-09-27',
      imageSrc: 'assets/hotel8new.jpg'
    }
    // Add more hotels here if needed...
  ];
  private reservations: any[] = [
    {
      hotelId: 1,
      checkInDate: '2023-09-16',
      checkOutDate: '2023-09-18',
      roomsReserved: 2
    },
    {
      hotelId: 1,
      checkInDate: '2023-09-18',
      checkOutDate: '2023-09-22',
      roomsReserved: 1
    },
    // Add more reservations here
  ];

  getAllHotels() {
    return this.hotels;
  }

  getHotelById(id: number) {
    return this.hotels.find((hotel) => hotel.id === id);
  }
  checkAvailability(hotelId: number, checkInDate: string, checkOutDate: string): boolean {
    const hotel = this.hotels.find(h => h.id === hotelId);

    if (!hotel) {
      return false; // Hotel not found
    }

    const reservationsForHotel = this.reservations.filter(r => r.hotelId === hotelId);
    const startDate = new Date(checkInDate);
    const endDate = new Date(checkOutDate);

    // Check if rooms are available between the specified dates
    let totalReservedRooms = 0;

    for (const reservation of reservationsForHotel) {
      const resCheckInDate = new Date(reservation.checkInDate);
      const resCheckOutDate = new Date(reservation.checkOutDate);

      // Check if there's any overlap between reservation dates and requested dates
      if (
        (startDate >= resCheckInDate && startDate < resCheckOutDate) ||
        (endDate > resCheckInDate && endDate <= resCheckOutDate)
      ) {
        totalReservedRooms += reservation.roomsReserved;
      }
    }

    const availableRooms = hotel.availableRooms - totalReservedRooms;

    return availableRooms > 0;
  }

  reserveRooms(hotelId: number, roomsToReserve: number): void {
    const hotel = this.hotels.find(h => h.id === hotelId);

    if (hotel) {
      if (roomsToReserve > 0 && roomsToReserve <= hotel.availableRooms) {
        // Check if there are enough available rooms to reserve
        hotel.availableRooms -= roomsToReserve; // Update availableRooms
      } else {
        console.log(`Not enough available rooms to reserve at ${hotel.name}.`);
      }
    } else {
      console.log(`Hotel with ID ${hotelId} not found.`);
    }
  }
}
