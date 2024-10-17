import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { PlaylistService } from '../services/playlist.service'; // Import PlaylistService
import { PlaylistListComponent } from './vendor-list.component'; // Adjust the import path
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Playlist } from '../models/playlist.model'; // Import Playlist model

describe('PlaylistListComponent', () => {
    let component: PlaylistListComponent;
    let fixture: ComponentFixture<PlaylistListComponent>;
    let mockPlaylistService: jasmine.SpyObj<PlaylistService>; // Specify the type of mock

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockPlaylistService = jasmine.createSpyObj<PlaylistService>('PlaylistService', ['getPlaylists', 'deletePlaylist'] as any);

        TestBed.configureTestingModule({
            declarations: [PlaylistListComponent],
            imports: [RouterTestingModule, HttpClientTestingModule],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: PlaylistService, useValue: mockPlaylistService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PlaylistListComponent);
        component = fixture.componentInstance;
    });

    fit('should_create_PlaylistListComponent', () => { // Change the function name
        expect(component).toBeTruthy();
    });

    fit('PlaylistListComponent_should_call_loadPlaylists_on_ngOnInit', () => { // Change the function name
        spyOn(component, 'loadPlaylists'); // Adjust the method name
        fixture.detectChanges();
        expect(component.loadPlaylists).toHaveBeenCalled(); // Adjust the method name
    });

    fit('PlaylistListComponent_should_have_searchPlaylists_method', () => {
        expect(component.searchPlaylists).toBeDefined();
      }); 

});
