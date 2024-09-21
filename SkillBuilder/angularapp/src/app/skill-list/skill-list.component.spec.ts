import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import {
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { AddSkillComponent } from '../add-skill/add-skill.component'; 
import { SkillService } from '../services/skill.service'; 
import { of } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';

describe('AddSkillComponent', () => {
  let component: AddSkillComponent;
  let fixture: ComponentFixture<AddSkillComponent>;
  let service: SkillService;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddSkillComponent],
      imports: [ReactiveFormsModule, HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [SkillService],
    });

    fixture = TestBed.createComponent(AddSkillComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(SkillService);
    router = TestBed.inject(Router);

    spyOn(router, 'navigateByUrl').and.returnValue(Promise.resolve(true));
    fixture.detectChanges();
  });

  it('should_create_AddSkillComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should_add_a_new_skill_when_form_is valid', fakeAsync(() => {
    const validSkillData = {
      title: 'Advanced Angular',
      modules_count: 10,
      description: 'In-depth Angular training',
      duration: '30 hours',
      targetSkillLevel: 'Intermediate',
    };

    spyOn(service, 'addSkill').and.returnValue(of(validSkillData));
    component.skillForm.setValue(validSkillData);
    
    expect(component.skillForm.valid).toBeTruthy();
    
    component.addSkill(); 
    tick();
    
    expect(service.addSkill).toHaveBeenCalledWith(validSkillData);
    expect(router.navigateByUrl).toHaveBeenCalledWith('/skills'); // Adjust based on your route
  }));

  it('should require all fields', () => {
    const form = component.skillForm;
    form.setValue({
      title: '',
      modules_count: '',
      description: '',
      duration: '',
      targetSkillLevel: '',
    });

    expect(form.valid).toBeFalsy();
    expect(form.get('title')?.hasError('required')).toBeTruthy();
    expect(form.get('modules_count')?.hasError('required')).toBeTruthy();
    expect(form.get('description')?.hasError('required')).toBeTruthy();
    expect(form.get('duration')?.hasError('required')).toBeTruthy();
    expect(form.get('targetSkillLevel')?.hasError('required')).toBeTruthy();
  });

  fit('should validate modules count', () => {
    const skillForm = component.skillForm;

    skillForm.setValue({
      title: 'Basic JavaScript',
      modules_count: 5,
      description: 'Fundamentals of JavaScript',
      duration: '10 hours',
      targetSkillLevel: 'Beginner',
    });
    expect(skillForm.valid).toBeTruthy();
    
    skillForm.setValue({
      title: 'Advanced JavaScript',
      modules_count: 1,
      description: 'Advanced JavaScript concepts',
      duration: '15 hours',
      targetSkillLevel: 'Intermediate',
    });
    expect(skillForm.valid).toBeFalsy();
    expect(skillForm.get('modules_count')?.hasError('min')).toBeTruthy();

    skillForm.setValue({
      title: 'Complex JavaScript',
      modules_count: 201,
      description: 'Complex JavaScript patterns',
      duration: '20 hours',
      targetSkillLevel: 'Expert',
    });
    expect(skillForm.valid).toBeFalsy();
    expect(skillForm.get('modules_count')?.hasError('max')).toBeTruthy();
  });
});
