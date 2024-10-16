import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CheeseShopService } from '../services/cheese-shop.service'; // Import CheeseShopService
import { CheeseShopListComponent } from './cheese-shop-list.component'; // Adjust the import path
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CheeseShop } from '../models/cheese-shop'; // Import CheeseShop model

describe('CheeseShopListComponent', () => {
    let component: CheeseShopListComponent;
    let fixture: ComponentFixture<CheeseShopListComponent>;
    let mockCheeseShopService: jasmine.SpyObj<CheeseShopService>; // Specify the type of mock

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockCheeseShopService = jasmine.createSpyObj<CheeseShopService>('CheeseShopService', ['getCheeseShops', 'searchCheeseShops', 'deleteCheeseShop'] as any);

        TestBed.configureTestingModule({
            declarations: [CheeseShopListComponent],
            imports: [RouterTestingModule, HttpClientTestingModule],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: CheeseShopService, useValue: mockCheeseShopService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(CheeseShopListComponent);
        component = fixture.componentInstance;
    });

    fit('should_create_CheeseShopListComponent', () => {
        expect((component as any)).toBeTruthy();
    });

    fit('CheeseShopListComponent_should_call_loadCheeseShops_on_ngOnInit', () => {
        spyOn((component as any), 'loadCheeseShops'); // Adjust the method name
        fixture.detectChanges();
        expect((component as any).loadCheeseShops).toHaveBeenCalled(); // Adjust the method name
    });

    fit('CheeseShopListComponent_should_have_searchCheeseShops_method', () => {
        expect((component as any).searchCheeseShops).toBeDefined();
    });
});