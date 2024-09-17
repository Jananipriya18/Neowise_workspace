import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import {
  FormBuilder,
  ReactiveFormsModule,
  FormsModule,
  Validators,
} from '@angular/forms';
import { AddSkillComponent } from '../add-skill/add-skill.component'; // Updated component name
import { SkillService } from '../services/skill.service'; // Updated service
import { of } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { DebugElement } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';

describe('AddSkillComponent', () => {
  let component: AddSkillComponent;
  let fixture: ComponentFixture<AddSkillComponent>;
  let service: SkillService; // Updated service
  let debugElement: DebugElement;
  let formBuilder: FormBuilder;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddSkillComponent],
      imports: [ReactiveFormsModule, HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [SkillService], // Updated service
    });
    formBuilder = TestBed.inject(FormBuilder) as any;
    fixture = TestBed.createComponent(AddSkillComponent) as any;
    component = fixture.componentInstance as any;
    service = TestBed.inject(SkillService) as any; // Updated service
    fixture.detectChanges();
    router = TestBed.inject(Router);
    spyOn(router, 'navigateByUrl').and.returnValue(Promise.resolve(true));
  });

  fit('should_create_AddSkillComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should_add_a_new_skill_when_form_is_valid', fakeAsync(() => {
    const validSkillData = {
      title: 'Advanced Angular',
      modules_count: 10,
      description: 'In-depth Angular training',
      duration: '30 hours',
      targetSkillLevel: 'Intermediate',
    };
    spyOn((service as any), 'addSkill').and.returnValue(of(validSkillData)); // Updated method name
    (component as any).skillForm.setValue(validSkillData); 
    let value: boolean = (component as any).skillForm.valid;
    (component as any).addSkill(); // Updated method name
    tick();
    expect(value).toBeTruthy();
    expect((service as any).addSkill).toHaveBeenCalledWith(validSkillData); // Updated method name
  }));

  fit('should_add_all_the_required_fields', () => {
    const form = (component as any).skillForm;
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

  fit('should_validate_modules_count', () => {
    const skillForm = (component as any).skillForm;
    skillForm.setValue({
      title: 'Basic JavaScript',
      modules_count: 5,
      description: 'Fundamentals of JavaScript',
      duration: '10 hours',
      targetSkillLevel: 'Beginner',
    });
    expect(skillForm.valid).toBeTruthy();
    expect(skillForm.get('modules_count')?.hasError('min')).toBeFalsy();
    expect(skillForm.get('modules_count')?.hasError('max')).toBeFalsy();
    skillForm.setValue({
      title: 'Advanced JavaScript',
      modules_count: 1,
      description: 'Advanced JavaScript concepts',
      duration: '15 hours',
      targetSkillLevel: 'Advanced',
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
