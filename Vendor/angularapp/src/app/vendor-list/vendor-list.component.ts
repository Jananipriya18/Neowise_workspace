// vendor-list.component.ts
import { Component, OnInit } from '@angular/core';
import { VendorService } from '../services/vendor.service'; // Import VendorService
import { Router } from '@angular/router';
import { Vendor } from '../models/vendor.model';

@Component({
  selector: 'app-vendor-list', // Changed selector to 'app-vendor-list'
  templateUrl: './vendor-list.component.html', // Adjusted the template URL
  styleUrls: ['./vendor-list.component.css'] // Adjusted the style URL
})
export class VendorListComponent implements OnInit {
  vendors: Vendor[] = []; // Changed recipes to vendors
  searchTerm: string = '';

  constructor(private vendorService: VendorService, private router: Router) { } // Adjusted service name

  ngOnInit(): void {
    this.loadVendors(); // Adjusted the method name
  }

  loadVendors(): void {
    this.vendorService.getVendors().subscribe(vendors => this.vendors = vendors); // Adjusted the service method name
  }

  deleteVendor(vendorId: number): void { // Adjusted the method name and parameter
    // Navigate to confirm delete page with the vendor ID as a parameter
    this.router.navigate(['/confirmDelete', vendorId]);
  }
  searchVendors(): void {
    if (this.searchTerm) {
      this.vendorService.searchVendors(this.searchTerm).subscribe(vendors => this.vendors = vendors);
    } else {
      this.loadVendors();
    }
  }
}
