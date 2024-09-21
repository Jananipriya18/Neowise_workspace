import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { SkillService } from './skill.service'; // Updated service

describe('SkillService', () => {
  let service: SkillService; // Updated service
  let httpTestingController: HttpTestingController;

  const mockSkills = [
    {
      id: 1,
      title: 'Advanced Angular',
      modules_count: 10,
      description: 'In-depth Angular training',
      duration: '30 hours',
      targetSkillLevel: 'Intermediate',
    },
    {
      id: 2,
      title: 'Basic JavaScript',
      modules_count: 5,
      description: 'Fundamentals of JavaScript',
      duration: '10 hours',
      targetSkillLevel: 'Beginner',
    },
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [SkillService], // Updated service
    });
    service = TestBed.inject(SkillService); // Updated service
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    // Ensure that there are no outstanding requests after each test
    httpTestingController.verify();
  });

  fit('should_create_service_skill', () => {
    expect(service).toBeTruthy();
  });

  fit('should_retrieve_skills_from_the_API_via_GET', () => {
    (service as any).getSkills().subscribe((skills) => {
      expect(skills).toEqual(mockSkills);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}`);
    expect(req.request.method).toEqual('GET');
    req.flush(mockSkills);
  });

  fit('should_add_a_skill_via_POST', () => {
    const newSkill = {
      title: 'Introduction to TypeScript',
      modules_count: 7,
      description: 'Beginner TypeScript course',
      duration: '15 hours',
      targetSkillLevel: 'Beginner',
    };
    (service as any).addSkill(newSkill).subscribe((skill) => {
      expect(skill).toEqual(newSkill);
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}`);
    expect(req.request.method).toEqual('POST');
    req.flush(newSkill);
  });

  fit('should_delete_a_skill_via_DELETE', () => {
    const skillId = 1;
    (service as any).deleteSkill(skillId).subscribe(() => {
    });
    const req = httpTestingController.expectOne(`${service['backendUrl']}/${skillId}`);
    expect(req.request.method).toEqual('DELETE');
    req.flush({});
  });
});
