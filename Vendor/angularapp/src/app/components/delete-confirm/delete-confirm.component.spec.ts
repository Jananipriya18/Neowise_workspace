import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DeleteConfirmComponent } from './delete-confirm.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { VendorService } from '../../services/vendor.service'; 

describe('DeleteConfirmComponent', () => {
    let component: DeleteConfirmComponent;
    let fixture: ComponentFixture<DeleteConfirmComponent>;
    let router: Router;
    let activatedRoute: ActivatedRoute;
    let mockVendorService: jasmine.SpyObj<VendorService>;

    beforeEach(waitForAsync(() => {
        mockVendorService = jasmine.createSpyObj<VendorService>('VendorService', ['getVendor', 'deleteVendor'] as any); 

        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpClientModule, FormsModule, HttpClientTestingModule],
            declarations: [DeleteConfirmComponent],
            providers: [
                { provide: VendorService, useValue: mockVendorService }
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
        
        mockVendorService.deleteVendor.and.returnValue(of(null)); 

        (component as any).confirmDelete(vendorId); 
        expect(mockVendorService.deleteVendor).toHaveBeenCalledWith(vendorId); 
    });
});

