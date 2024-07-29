import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CartoonEpisodeService } from '../services/cartoon-episode.service'; // Import CartoonEpisodeService
import { CartoonEpisodeListComponent } from './cartoon-episode-list.component'; // Adjust the import path
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CartoonEpisode } from '../models/cartoon-episode.model'; // Import CartoonEpisode model

describe('CartoonEpisodeListComponent', () => {
    let component: CartoonEpisodeListComponent;
    let fixture: ComponentFixture<CartoonEpisodeListComponent>;
    let mockCartoonEpisodeService: jasmine.SpyObj<CartoonEpisodeService>; // Specify the type of mock

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockCartoonEpisodeService = jasmine.createSpyObj<CartoonEpisodeService>('CartoonEpisodeService', ['getCartoonEpisodes', 'searchCartoonEpisodes', 'deleteCartoonEpisode'] as any);

        TestBed.configureTestingModule({
            declarations: [CartoonEpisodeListComponent],
            imports: [RouterTestingModule, HttpClientTestingModule],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: CartoonEpisodeService, useValue: mockCartoonEpisodeService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(CartoonEpisodeListComponent);
        component = fixture.componentInstance;
    });

    fit('should_create_CartoonEpisodeListComponent', () => {
        expect(component).toBeTruthy();
    });

    fit('CartoonEpisodeListComponent_should_call_loadCartoonEpisodes_on_ngOnInit', () => {
        spyOn(component, 'loadCartoonEpisodes'); // Adjust the method name
        fixture.detectChanges();
        expect(component.loadCartoonEpisodes).toHaveBeenCalled(); // Adjust the method name
    });

    fit('CartoonEpisodeListComponent_should_have_searchCartoonEpisodes_method', () => {
        expect(component.searchCartoonEpisodes).toBeDefined();
    });
});