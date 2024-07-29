import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DeleteConfirmComponent } from './delete-confirm.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CartoonEpisodeService } from '../services/cartoon-episode.service'; // Adjusted service name
import { CartoonEpisode } from '../models/cartoon-episode.model'; // Adjusted model name

describe('DeleteConfirmComponent', () => {
    let component: DeleteConfirmComponent;
    let fixture: ComponentFixture<DeleteConfirmComponent>;
    let router: Router;
    let activatedRoute: ActivatedRoute;
    let mockCartoonEpisodeService: jasmine.SpyObj<CartoonEpisodeService>; // Adjusted service name

    beforeEach(waitForAsync(() => {
        mockCartoonEpisodeService = jasmine.createSpyObj<CartoonEpisodeService>('CartoonEpisodeService', ['getCartoonEpisode', 'deleteCartoonEpisode'] as any); // Adjusted service name and methods

        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpClientModule, FormsModule, HttpClientTestingModule],
            declarations: [DeleteConfirmComponent],
            providers: [
                { provide: CartoonEpisodeService, useValue: mockCartoonEpisodeService } // Adjusted service name
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

    fit('DeleteConfirmComponent_should_call_deleteCartoonEpisode_method_when_confirmDelete_is_called', () => {
        const episodeId = 1; // Adjusted ID name
        
        mockCartoonEpisodeService.deleteCartoonEpisode.and.returnValue(of(null)); // Adjusted method name

        component.confirmDelete(episodeId); // Adjusted parameter name

        expect(mockCartoonEpisodeService.deleteCartoonEpisode).toHaveBeenCalledWith(episodeId); // Adjusted method name and parameter
    });
});
