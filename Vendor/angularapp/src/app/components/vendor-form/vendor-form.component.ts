import { Component } from '@angular/core';
import { Vendor } from '../../models/vendor.model';
import { VendorService } from '../../services/vendor.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vendor-form',
  templateUrl: './vendor-form.component.html',
  styleUrls: ['./vendor-form.component.css']
})
export class VendorFormComponent {
  newVendor: Vendor = {
    vendorId: 0,
    name: '',
    productOfferings: '',
    experience: '',
    storeLocation: '',
    operatingHours: '',
    phoneNumber: ''
  }; // Initialize newVendor with empty fields

  formSubmitted = false; // Track form submission

  constructor(private vendorService: VendorService, private router: Router) { }

  addVendor(): void {
    this.formSubmitted = true; // Set formSubmitted to true on form submission

    // Validate if any required field is empty
    if (!this.newVendor.name || !this.newVendor.productOfferings || 
        !this.newVendor.experience || !this.newVendor.storeLocation || 
        !this.newVendor.operatingHours || !this.newVendor.phoneNumber) {
      console.log('Form is invalid.');
      return;
    }

    // If form is valid, add the vendor
    this.vendorService.addVendor(this.newVendor).subscribe(() => {
      console.log('Vendor added successfully!');
      this.router.navigate(['/viewVendors']);
    });
  }
}
