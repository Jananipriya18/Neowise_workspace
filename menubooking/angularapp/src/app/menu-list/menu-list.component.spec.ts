import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { MenuService } from '../services/menu.service';
import { MenuListComponent } from './menu-list.component';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Menu } from '../models/menu.model';

describe('MenuListComponent', () => {
    let component: MenuListComponent;
    let fixture: ComponentFixture<MenuListComponent>;
    let mockMenuService: jasmine.SpyObj<MenuService>; // Specify the type of mock

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockMenuService = jasmine.createSpyObj<MenuService>('MenuService', ['getMenus', 'addMenu']);

        TestBed.configureTestingModule({
            declarations: [MenuListComponent],
            imports: [RouterTestingModule, HttpClientTestingModule],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: MenuService, useValue: mockMenuService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(MenuListComponent);
        component = fixture.componentInstance;
    });

    fit('should_create_menu_listComponent', () => {
        expect(component).toBeTruthy();
    });

    fit('menu_listComponent_should_call_loadMenus_on_ngOnInit', () => {
        spyOn(component, 'loadMenus');
        fixture.detectChanges();
        expect(component.loadMenus).toHaveBeenCalled();
    });

    // fit('should_add_a_menu_to_the_database', () => {
    //     const newMenu: Menu = { menuId: 1, name: 'New Menu', email: 'Email', subjectsOffered: 'SubjectsOffered', contactNumber: 'ContactNumber', availability: 'Availability' };
    //     const initialDatabaseLength = 0; // Assuming there are initially 2 menus in the database
    
    //     mockMenuService.addMenu.and.returnValue(of()); // Mock addMenu method to return a successful response
    //     mockMenuService.getMenus.and.returnValue(of([...component.menus, newMenu])); // Mock getMenus to return the updated list with the new menu
    
    //     component.menus = []; // Clear menus array for testing
    //     component.loadMenus(); // Load menus
    
    //     expect(mockMenuService.addMenu).toHaveBeenCalledWith(newMenu); // Verify addMenu method is called with new menu
    //     expect(component.menus.length).toBe(initialDatabaseLength + 1); // Check if a menu is added by checking the length
    // });

});
