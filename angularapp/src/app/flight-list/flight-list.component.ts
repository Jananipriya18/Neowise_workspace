import { Component, OnInit } from '@angular/core';
import { FlightService } from '../../services/flight.service';

@Component({
  selector: 'app-flight-list',
  templateUrl: './flight-list.component.html',
  styleUrls: ['./flight-list.component.css']
})
export class FlightListComponent implements OnInit {
  flights!: any[];
  filteredFlights!: any[];
  searchText: string = '';

  constructor(private flightService: FlightService) {}

  ngOnInit() {
    this.flights = this.flightService.getAllFlights();
    this.filteredFlights = [...this.flights];
  }

  searchFlights() {
    this.filteredFlights = this.flights.filter(flight =>
      flight.name.toLowerCase().includes(this.searchText.toLowerCase()) ||
      flight.airline.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }
}
