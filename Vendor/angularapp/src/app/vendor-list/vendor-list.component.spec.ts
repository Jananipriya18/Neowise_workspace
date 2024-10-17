import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { VendorService } from '../services/vendor.service'; // Import VendorService
import { VendorListComponent } from './vendor-list.component'; // Adjust the import path
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Vendor } from '../models/vendor.model'; // Import Vendor model

describe('VendorListComponent', () => {
    let component: VendorListComponent;
    let fixture: ComponentFixture<VendorListComponent>;
    let mockVendorService: jasmine.SpyObj<VendorService>; // Specify the type of mock

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockVendorService = jasmine.createSpyObj<VendorService>('VendorService', ['getVendors', 'deleteVendor'] as any);

        TestBed.configureTestingModule({
            declarations: [VendorListComponent],
            imports: [RouterTestingModule, HttpClientTestingModule],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: VendorService, useValue: mockVendorService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(VendorListComponent);
        component = fixture.componentInstance;
    });

    fit('should_create_VendorListComponent', () => { // Change the function name
        expect((component as any)).toBeTruthy();
    });

    fit('VendorListComponent_should_call_loadVendors_on_ngOnInit', () => { // Change the function name
        spyOn((component as any), 'loadVendors'); // Adjust the method name
        fixture.detectChanges();
        expect((component as any).loadVendors).toHaveBeenCalled(); // Adjust the method name
    });

    fit('VendorListComponent_should_have_searchVendors_method', () => {
        expect((component as any).searchVendors).toBeDefined();
      }); 

});
