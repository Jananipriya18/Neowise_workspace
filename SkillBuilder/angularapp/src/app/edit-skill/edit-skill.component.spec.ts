import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EditSkillComponent } from './edit-skill.component'; // Updated component
import { SkillService } from '../services/skill.service'; // Updated service
import { of, throwError } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Skill } from '../model/skill.model'; // Updated model

describe('EditSkillComponent', () => {
  let component: EditSkillComponent;
  let fixture: ComponentFixture<EditSkillComponent>;
  let service: SkillService; // Updated service
  let routerSpy: jasmine.SpyObj<Router>;

  const mockSkill: Skill = {
    id: 1,
    title: 'Advanced Angular',
    modules_count: 10,
    description: 'In-depth Angular training',
    duration: '30 hours',
    targetSkillLevel: 'Intermediate',
  };

  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [EditSkillComponent], // Updated component
      imports: [HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [
        SkillService, // Updated service
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => '1' } } } },
        { provide: Router, useValue: spy }
      ]
    });

    fixture = TestBed.createComponent(EditSkillComponent); // Updated component
    component = fixture.componentInstance;
    service = TestBed.inject(SkillService); // Updated service
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should_create_EditSkillComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_load_skill_details_on_init', () => {
    spyOn(service, 'getSkillById').and.returnValue(of(mockSkill)); // Updated method
    component.ngOnInit();
    fixture.detectChanges();

    expect(service.getSkillById).toHaveBeenCalledWith(1); // Updated method
    expect(component.skill).toEqual(mockSkill); // Updated variable
  });

  fit('should_save_skill_details', () => {
    spyOn(service, 'updateSkill').and.returnValue(of(mockSkill)); // Updated method
    component.skill = { ...mockSkill }; // Updated variable
    component.saveSkill(); // Updated method
    
    expect(service.updateSkill).toHaveBeenCalledWith(mockSkill.id, mockSkill); // Updated method
  });

});
