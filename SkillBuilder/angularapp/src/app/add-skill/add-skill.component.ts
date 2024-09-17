import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SkillService } from '../services/skill.service'; // Adjust the path as necessary
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-skill',
  templateUrl: './add-skill.component.html',
  styleUrls: ['./add-skill.component.css']
})
export class AddSkillComponent implements OnInit {
  skillForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private skillService: SkillService,    private router: Router
    ) {
    this.skillForm = this.formBuilder.group({
      title: ['', Validators.required],
      modules_count: ['', [Validators.required, Validators.min(2), Validators.max(200)]], // Updated age range
      description: ['', Validators.required],
      duration: ['', Validators.required],
      targetSkillLevel: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {}

  addSkill(): void {
    if (this.skillForm.valid) {
      console.log(this.skillForm.value);
      this.skillService.addSkill(this.skillForm.value)
        .subscribe(
          (res) => {
            console.log('Skill added successfully:', res);
            this.router.navigateByUrl('/skillslist');
            // Optionally reset the form or show a success message
            this.skillForm.reset();
          },
          (err) => {
            console.error('Error adding skill:', err);
            // Handle error, show error message to the user
          }
        );
    } else {
      console.log('Form is invalid');
    }
  }
}
