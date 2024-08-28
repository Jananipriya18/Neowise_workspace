import { Component, OnInit } from '@angular/core';
import { HotelService } from '../../services/hotel.service';

@Component({
  selector: 'app-hotel-list',
  templateUrl: './hotel-list.component.html',
  styleUrls: ['./hotel-list.component.css']
})
export class HotelListComponent implements OnInit {
  hotels!: any[];
  filteredHotels!: any[];
  searchText: string = '';

  constructor(private hotelService: HotelService) {}

  ngOnInit() {
    this.hotels = this.hotelService.getAllHotels();
    this.filteredHotels = [...this.hotels];
  }

  searchHotels() {
    this.filteredHotels = this.hotels.filter(hotel =>
      hotel.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }
}
