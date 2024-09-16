import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EditStylistComponent } from './edit-stylist.component';
import { StylistService } from '../services/stylist.service';
import { of, throwError } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Stylist } from '../model/stylist.model';

describe('EditStylistComponent', () => {
  let component: EditStylistComponent;
  let fixture: ComponentFixture<EditStylistComponent>;
  let service: StylistService;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockStylist: Stylist = {
    id: 1,
    name: 'Jane Doe',
    expertise: 'Fashion Consulting',
    styleSignature: 'Chic and Elegant',
    availability: 'Full-time',
    hourlyRate: 100,
    location: 'New York'
  };


  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [EditStylistComponent],
      imports: [HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [
        StylistService,
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => '1' } } } },
        { provide: Router, useValue: spy }
      ]
    });

    fixture = TestBed.createComponent(EditStylistComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(StylistService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should_create_EditStylistComponent', () => {
    expect((component as any)).toBeTruthy();
  });

  fit('should_load_stylist_details_on_init', () => {
    spyOn((service as any), 'getStylistById').and.returnValue(of(mockStylist));
    (component as any).ngOnInit();
    fixture.detectChanges();

    expect((service as any).getStylistById).toHaveBeenCalledWith(1);
    expect((component as any).stylist).toEqual(mockStylist);
  });

  fit('should_save_stylist_details', () => {
    spyOn((service as any), 'updateStylist').and.returnValue(of(mockStylist));
    (component as any).stylist = { ...mockStylist };
    (component as any).saveStylist();
    
    expect((service as any).updateStylist).toHaveBeenCalledWith(mockStylist.id, mockStylist);
  });

});
