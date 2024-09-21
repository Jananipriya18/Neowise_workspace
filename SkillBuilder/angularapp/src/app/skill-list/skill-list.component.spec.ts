import { ComponentFixture, TestBed } from '@angular/core/testing'; 
import { SkillListComponent } from './skill-list.component';
import { SkillService } from '../services/skill.service';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { Skill } from '../model/skill.model';
import { By } from '@angular/platform-browser';

describe('SkillListComponent', () => {
  let component: SkillListComponent;
  let fixture: ComponentFixture<SkillListComponent>;
  let service: SkillService;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockSkills: Skill[] = [
    {
      id: 1,
      title: 'Tech Talks',
      modules_count: 5,
      description: 'A skill about technology trends.',
      duration: '30 mins',
      targetSkillLevel: 'Beginner',
    },
    {
      id: 2,
      title: 'Science Weekly',
      modules_count: 8,
      description: 'A skill about science.',
      duration: '45 mins',
      targetSkillLevel: 'Expert',
    },
    {
      id: 3,
      title: 'Art History',
      modules_count: 6,
      description: 'An overview of significant art movements.',
      duration: '50 mins',
      targetSkillLevel: 'Intermediate',
    },
    {
      id: 4,
      title: 'Advanced Mathematics',
      modules_count: 10,
      description: 'In-depth look at complex mathematical concepts.',
      duration: '60 mins',
      targetSkillLevel: 'Expert',
    },
  ];
  
  beforeEach(() => {
    const spy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [SkillListComponent],
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [SkillService, { provide: Router, useValue: spy }]
    });

    fixture = TestBed.createComponent(SkillListComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(SkillService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  fit('should_create_SkillListComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_call_getSkills', () => {
    spyOn(service, 'getSkills').and.returnValue(of(mockSkills)); // Mock the service call
    component.ngOnInit();
    expect(service.getSkills).toHaveBeenCalled();
    expect(component.skills).toEqual(mockSkills);
  });

  fit('should_call_deleteSkill', () => {
    spyOn((service as any), 'deleteSkill').and.returnValue(of());
    (component as any).deleteSkill(1);
    expect((service as any).deleteSkill).toHaveBeenCalledWith(1);
  });

  fit('should_sort_skills_by_targetSkillLevel', () => {
    // Assign the mockSkills to the component's skills
    component.skills = mockSkills;
  
    // Sort in ascending order
    component.sortSkills();
    expect(component.skills[0].targetSkillLevel).toBe('Beginner');
    expect(component.skills[1].targetSkillLevel).toBe('Intermediate');
    expect(component.skills[2].targetSkillLevel).toBe('Expert');
    expect(component.skills[3].targetSkillLevel).toBe('Expert'); // Note: Two 'Expert' levels, maintain order
  
    // Sort in descending order
    component.sortSkills();
    expect(component.skills[0].targetSkillLevel).toBe('Expert');
    expect(component.skills[1].targetSkillLevel).toBe('Expert');
    expect(component.skills[2].targetSkillLevel).toBe('Intermediate');
    expect(component.skills[3].targetSkillLevel).toBe('Beginner');
  });
  
});
