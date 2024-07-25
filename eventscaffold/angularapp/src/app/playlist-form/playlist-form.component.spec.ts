import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { PlaylistFormComponent } from './playlist-form.component';
import { PlaylistService } from '../services/playlist.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { Playlist } from '../models/playlist.model';

describe('PlaylistFormComponent', () => {
  let component: PlaylistFormComponent;
  let fixture: ComponentFixture<PlaylistFormComponent>;
  let playlistService: PlaylistService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PlaylistFormComponent],
      imports: [FormsModule, RouterTestingModule, HttpClientTestingModule],
      providers: [PlaylistService]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaylistFormComponent);
    component = fixture.componentInstance;
    playlistService = TestBed.inject(PlaylistService);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  fit('should_create_PlaylistFormComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('PlaylistFormComponent_should_render_error_messages_when_required_fields_are_empty_on_submit', () => {
    // Set all fields to empty strings
    component.newPlaylist = {
      playlistId: 0,
      playlistName: '',
      songName: '',
      yearOfRelease: '',
      artistName: '',
      genre: '',
      MovieName: ''
    } as Playlist;

    // Manually trigger form submission
    component.formSubmitted = true;
    fixture.detectChanges();

    // Find the form element
    const form = fixture.debugElement.query(By.css('form')).nativeElement;

    // Trigger the form submission
    form.dispatchEvent(new Event('submit'));

    fixture.detectChanges();

    // Check if error messages are rendered for each field
    expect(fixture.debugElement.query(By.css('#playlistName + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#songName + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#yearOfRelease + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#artistName + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#genre + .error-message'))).toBeTruthy();
    expect(fixture.debugElement.query(By.css('#MovieName + .error-message'))).toBeTruthy();
  });

  fit('PlaylistFormComponent_should_call_addPlaylist_method_while_adding_the_playlist', () => {
    // Create a mock Playlist object with all required properties
    const playlist: Playlist = {
      playlistId: 1,
      playlistName: 'Test Playlist Name',
      songName: 'Test Playlist Description',
      yearOfRelease: 'Test Playlist Date',
      artistName: 'Test Playlist Time',
      genre: 'Test Playlist Location',
      MovieName: 'Test Playlist Organizer'
    };

    const addPlaylistSpy = spyOn(component, 'addPlaylist').and.callThrough();
    component.addPlaylist();
    expect(addPlaylistSpy).toHaveBeenCalled();
  });
});
