import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DeleteConfirmComponent } from './delete-confirm.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CartoonEpisodeService } from '../services/cartoonepisode.service'; 
import { CartoonEpisode } from '../models/cartoonepisode.model'; 

describe('DeleteConfirmComponent', () => {
    let component: DeleteConfirmComponent;
    let fixture: ComponentFixture<DeleteConfirmComponent>;
    let router: Router;
    let activatedRoute: ActivatedRoute;
    let mockCartoonEpisodeService: jasmine.SpyObj<CartoonEpisodeService>; 

    beforeEach(waitForAsync(() => {
        mockCartoonEpisodeService = jasmine.createSpyObj<CartoonEpisodeService>('CartoonEpisodeService', ['getCartoonEpisode', 'deleteCartoonEpisode'] as any); 

        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpClientModule, FormsModule, HttpClientTestingModule],
            declarations: [DeleteConfirmComponent],
            providers: [
                { provide: CartoonEpisodeService, useValue: mockCartoonEpisodeService } 
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
        const episodeId = 1; 
        
        mockCartoonEpisodeService.deleteCartoonEpisode.and.returnValue(of(null)); 

        component.confirmDelete(episodeId); 

        expect(mockCartoonEpisodeService.deleteCartoonEpisode).toHaveBeenCalledWith(episodeId); 
    });
});
