import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VendorService } from '../services/vendor.service' // Adjusted service name
import { Vendor } from '../models/vendor.model'; // Adjusted model name

@Component({
  selector: 'app-delete-confirm',
  templateUrl: './delete-confirm.component.html',
  styleUrls: ['./delete-confirm.component.css']
})
export class DeleteConfirmComponent implements OnInit {
  vendorId: number;
  vendor: Vendor = {} as Vendor; // Initialize vendor property with an empty object

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private vendorService: VendorService // Adjusted service name
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.vendorId = +params['id']; // Adjust parameter name to 'id' if it matches the route parameter
      this.vendorService.getVendor(this.vendorId).subscribe(
        (vendor: Vendor) => {
          this.vendor = vendor;
        },
        error => {
          console.error('Error fetching vendor:', error);
        }
      );
    });
  }

  confirmDelete(vendorId: number): void { // Adjust method signature
    this.vendorService.deleteVendor(vendorId).subscribe(
      () => {
        console.log('Vendor deleted successfully.');
        this.router.navigate(['/viewVendors']); // Adjust the route to navigate after deletion
      },
      (error) => {
        console.error('Error deleting vendor:', error);
      }
    );
  }

  cancelDelete(): void {
    this.router.navigate(['/viewVendors']); // Adjust the route to navigate on cancel
  }
}
