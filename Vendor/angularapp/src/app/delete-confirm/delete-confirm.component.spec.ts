import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DeleteConfirmComponent } from './delete-confirm.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { VendorService } from '../services/vendor.service'; // Adjusted service name
import { Vendor } from '../models/vendor.model'; // Adjusted model name

describe('DeleteConfirmComponent', () => {
    let component: DeleteConfirmComponent;
    let fixture: ComponentFixture<DeleteConfirmComponent>;
    let router: Router;
    let activatedRoute: ActivatedRoute;
    let mockVendorService: jasmine.SpyObj<VendorService>; // Adjusted service name

    beforeEach(waitForAsync(() => {
        mockVendorService = jasmine.createSpyObj<VendorService>('VendorService', ['getVendor', 'deleteVendor'] as any); // Adjusted service name and methods

        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpClientModule, FormsModule, HttpClientTestingModule],
            declarations: [DeleteConfirmComponent],
            providers: [
                { provide: VendorService, useValue: mockVendorService } // Adjusted service name
            ]
        }).compileComponents();
        router = TestBed.inject(Router);
        activatedRoute = TestBed.inject(ActivatedRoute);
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(DeleteConfirmComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    fit('should_create_DeleteConfirmComponent', () => {
        expect((component as any)).toBeTruthy();
    });

    fit('DeleteConfirmComponent_should_call_deleteVendor_method_when_confirmDelete_is_called', () => {
        const vendorId = 1; // Adjusted ID name
        
        mockVendorService.deleteVendor.and.returnValue(of(null)); // Adjusted method name

        (component as any).confirmDelete(vendorId); // Adjusted parameter name

        expect(mockVendorService.deleteVendor).toHaveBeenCalledWith(vendorId); // Adjusted method name and parameter
    });
});

