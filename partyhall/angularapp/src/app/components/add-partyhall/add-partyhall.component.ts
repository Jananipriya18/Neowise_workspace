import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PartyhallService } from 'src/app/services/partyhall.service';
import { PartyHall } from 'src/app/models/partyHall.model';

@Component({
  selector: 'app-add-partyhall',
  templateUrl: './add-partyhall.component.html',
  styleUrls: ['./add-partyhall.component.css']
})
export class AddPartyhallComponent implements OnInit {

  addPartyhallForm: FormGroup;
  errorMessage = '';
  photoImage = "";

  constructor(private fb: FormBuilder, private partyhallService: PartyhallService, private route: Router) {
    this.addPartyhallForm = this.fb.group({
      partyhallName: ['', Validators.required],
      partyhallImageUrl: [null, Validators.required],
      partyhallLocation: ['', Validators.required],
      description: ['', Validators.required],
      partyhallAvailableStatus: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(1)]],
      capacity: ['', [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit(): void { }

  onSubmit(): void {
    if (this.addPartyhallForm.valid) {
      const newPartyhall = this.addPartyhallForm.value;
      const requestObj: PartyHall = {
        hallName: newPartyhall.partyhallName,
        hallImageUrl: this.photoImage,
        hallLocation: newPartyhall.partyhallLocation,
        description: newPartyhall.description,
        hallAvailableStatus: newPartyhall.partyhallAvailableStatus,
        price: newPartyhall.price,
        capacity: newPartyhall.capacity,
      };

      this.partyhallService.addPartyHall(requestObj).subscribe(
        (response) => {
          console.log('Party Hall added successfully', response);
          this.route.navigate(['/admin/view/partyhall']);
          this.addPartyhallForm.reset(); // Reset the form
        },
        (error) => {
          console.error('Error adding party hall', error.error);
          this.errorMessage = error.error;
        }
      );
    } else {
      this.errorMessage = "All fields are required";
    }
  }

  handleFileChange(event: any): void {
    const file = event.target.files[0];

    if (file) {
      this.convertFileToBase64(file).then(
        (base64String) => {
          this.photoImage = base64String;
        },
        (error) => {
          console.error('Error converting file to base64:', error);
        }
      );
    }
  }

  convertFileToBase64(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();

      reader.onload = () => {
        resolve(reader.result as string);
      };

      reader.onerror = (error) => {
        reject(error);
      };

      reader.readAsDataURL(file);
    });
  }
}
