import { Component, OnInit } from '@angular/core';
import { Skill } from '../model/skill.model';
import { SkillService } from '../services/skill.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-skill-list',
  templateUrl: './skill-list.component.html',
  styleUrls: ['./skill-list.component.css'],
})
export class SkillListComponent implements OnInit {
  skills: Skill[] = [];

  constructor(private skillService: SkillService,private router: Router) {}

  ngOnInit(): void {
    this.getSkills();
  }

  getSkills(): void {
    try {
      this.skillService.getSkills().subscribe(
        (res) => {
          console.log(res);
          this.skills = res;
        },
        (err) => {
          console.log(err);
        }
      );
    } catch (err) {
      console.log('Error:', err);
    }
  }

  deleteSkill(id: any): void {
    this.skillService.deleteSkill(id).subscribe(() => {
      this.skills = this.skills.filter((skill) => skill.id !== id);
    });
  }
}
