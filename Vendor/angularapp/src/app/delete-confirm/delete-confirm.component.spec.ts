import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DeleteConfirmComponent } from './delete-confirm.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { PlaylistService } from '../services/playlist.service'; // Adjusted service name
import { Playlist } from '../models/playlist.model'; // Adjusted model name

describe('DeleteConfirmComponent', () => {
    let component: DeleteConfirmComponent;
    let fixture: ComponentFixture<DeleteConfirmComponent>;
    let router: Router;
    let activatedRoute: ActivatedRoute;
    let mockPlaylistService: jasmine.SpyObj<PlaylistService>; // Adjusted service name

    beforeEach(waitForAsync(() => {
        mockPlaylistService = jasmine.createSpyObj<PlaylistService>('PlaylistService', ['getPlaylist', 'deletePlaylist'] as any); // Adjusted service name and methods

        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpClientModule, FormsModule, HttpClientTestingModule],
            declarations: [DeleteConfirmComponent],
            providers: [
                { provide: PlaylistService, useValue: mockPlaylistService } // Adjusted service name
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

    fit('DeleteConfirmComponent_should_call_deletePlaylist_method_when_confirmDelete_is_called', () => {
        const playlistId = 1; // Adjusted ID name
        
        mockPlaylistService.deletePlaylist.and.returnValue(of(null)); // Adjusted method name

        component.confirmDelete(playlistId); // Adjusted parameter name

        expect(mockPlaylistService.deletePlaylist).toHaveBeenCalledWith(playlistId); // Adjusted method name and parameter
    });
});

