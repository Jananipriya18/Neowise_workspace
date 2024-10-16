import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DeleteConfirmComponent } from './delete-confirm.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CheeseShopService } from '../services/cheese-shop.service'; 
import { CheeseShop } from '../models/cheese-shop'; 

describe('DeleteConfirmComponent', () => {
    let component: DeleteConfirmComponent;
    let fixture: ComponentFixture<DeleteConfirmComponent>;
    let router: Router;
    let activatedRoute: ActivatedRoute;
    let mockCheeseShopService: jasmine.SpyObj<CheeseShopService>; 

    beforeEach(waitForAsync(() => {
        mockCheeseShopService = jasmine.createSpyObj<CheeseShopService>('CheeseShopService', ['getCheeseShop', 'deleteCheeseShop'] as any); 

        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpClientModule, FormsModule, HttpClientTestingModule],
            declarations: [DeleteConfirmComponent],
            providers: [
                { provide: CheeseShopService, useValue: mockCheeseShopService } 
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

    fit('DeleteConfirmComponent_should_call_deleteCheeseShop_method_when_confirmDelete_is_called', () => {
        const shopId = 1; 
        
        (mockCheeseShopService as any).deleteCheeseShop.and.returnValue(of(null)); 

        (component as any).confirmDelete(shopId); 

        expect((mockCheeseShopService as any).deleteCheeseShop).toHaveBeenCalledWith(shopId); 
    });
});
