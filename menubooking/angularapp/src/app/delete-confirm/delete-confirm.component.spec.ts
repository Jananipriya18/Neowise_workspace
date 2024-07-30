import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DeleteConfirmComponent } from './delete-confirm.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MenuService } from '../services/menu.service';
import { Menu } from '../models/menu.model'

describe('DeleteConfirmComponent', () => {
    let component: DeleteConfirmComponent;
    let fixture: ComponentFixture<DeleteConfirmComponent>;
    let router: Router;
    let activatedRoute: ActivatedRoute;
    let mockMenuService: jasmine.SpyObj<MenuService>; // Declare mockMenuService

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockMenuService = jasmine.createSpyObj<MenuService>('MenuService', ['getMenu', 'deleteMenu'] as any);

        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpClientModule, FormsModule, HttpClientTestingModule], // Add HttpClientModule and HttpClientTestingModule to imports
            declarations: [DeleteConfirmComponent],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: MenuService, useValue: mockMenuService }
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
        expect(component).toBeTruthy();
    });

    // fit('DeleteConfirmComponent_should_navigate_to_viewMenus_after_cancelDelete', () => {
    //     spyOn(router, 'navigate').and.stub(); // Spy on router.navigate method
    //     component.cancelDelete();
    //     expect(router.navigate).toHaveBeenCalledWith(['/viewMenus']); // Verify router.navigate is called with correct argument
    // });

    fit('DeleteConfirmComponent_should_call_deleteMenu_method_when_confirmDelete_is_called', () => {
        const menuId = 1;
        
        // Spy on the deleteMenu method of the MenuService
        mockMenuService.deleteMenu.and.returnValue(of(null));

        // Call the confirmDelete method
        component.confirmDelete(menuId);

        // Expect the deleteMenu method to have been called with the menuId
        expect(mockMenuService.deleteMenu).toHaveBeenCalledWith(menuId);
    });
});

