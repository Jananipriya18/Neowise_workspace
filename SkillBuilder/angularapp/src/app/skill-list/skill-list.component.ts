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
  loading: boolean = false;
  sortOrder: 'asc' | 'desc' = 'asc';

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

  sortSkills(): void {
    const orderMultiplier = this.sortOrder === 'asc' ? 1 : -1;
    const skillLevels = ['Beginner', 'Intermediate', 'Expert'];

    this.skills.sort((a, b) => {
      const levelA = skillLevels.indexOf(a.targetSkillLevel);
      const levelB = skillLevels.indexOf(b.targetSkillLevel);
      return (levelA - levelB) * orderMultiplier;
    });

    // Toggle sort order for the next click
    this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
  }

  deleteSkill(id: any): void {
    this.skillService.deleteSkill(id).subscribe(() => {
      this.skills = this.skills.filter((skill) => skill.id !== id);
    });
  }
}
