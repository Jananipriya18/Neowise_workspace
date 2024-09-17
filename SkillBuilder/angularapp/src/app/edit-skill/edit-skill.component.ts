// edit-skill.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Skill } from '../model/skill.model';
import { SkillService } from '../services/skill.service';

@Component({
  selector: 'app-edit-skill',
  templateUrl: './edit-skill.component.html',
  styleUrls: ['./edit-skill.component.css']
})
export class EditSkillComponent implements OnInit {
  skill: Skill | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private skillService: SkillService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.skillService.getSkillById(+id).subscribe(
        (skill) => (this.skill = skill),
        (err) => console.error(err)
      );
    }
  }

  saveSkill(): void {
    if (this.skill) {
      this.skillService.updateSkill(this.skill.id, this.skill).subscribe(
        () => {
          this.router.navigate(['/skillsList']);
        },
        (err) => {
          console.error(err);
        }
      );
    }
  }

  cancel(): void {
    this.router.navigate(['/skillsList']);
  }
}
