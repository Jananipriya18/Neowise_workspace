import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { PartyHall } from 'src/app/models/partyHall.model';
import { PartyhallService } from 'src/app/services/partyhall.service';

@Component({
  selector: 'app-admin-view-party-hall',
  templateUrl: './admin-view-party-hall.component.html',
  styleUrls: ['./admin-view-party-hall.component.css']
})
export class AdminViewPartyHallComponent implements OnInit {
  showDeletePopup = false;
  selectedPartyHall: PartyHall;
  isEditing = false;
  partyHalls: PartyHall[] = [];
  photoImage = '';
  addPartyHallForm: FormGroup;

  constructor(private fb: FormBuilder, private partyHallService: PartyhallService) {
    this.addPartyHallForm = this.fb.group({
      hallImageUrl: ['', Validators.required],
      // Add other form controls as needed
    });
  }

  ngOnInit(): void {
    this.getAllPartyHalls();
  }

  getAllPartyHalls() {
    this.partyHallService.getAllPartyHalls().subscribe(
      (data: PartyHall[]) => {
        this.partyHalls = data;
        console.log(this.partyHalls);
      },
      (err) => {
        console.log(err);
      }
    );
  }

  deletePartyHall(partyHallId: number) {
    this.partyHallService.deletePartyHall(partyHallId).subscribe(
      (data: any) => {
        console.log('Party Hall deleted successfully', data);
        this.getAllPartyHalls();
      },
      (err) => {
        console.log(err);
      }
    );
  }

  editPartyHall(partyHall: PartyHall) {

    console.log("came");
    this.selectedPartyHall = partyHall; // Create a copy to avoid direct modification
    this.isEditing = true;
    console.log("this.selectedPartyHall"+this.selectedPartyHall);
    
  }

  updatePartyHall(partyHallDetails: PartyHall) {
    if (this.photoImage) {
      partyHallDetails.hallImageUrl = this.photoImage;
    }

    this.partyHallService.updatePartyHall(partyHallDetails).subscribe(
      (data: any) => {
        console.log('Party Hall updated successfully', data);
        this.getAllPartyHalls();
        this.cancelEdit();
      },
      (err) => {
        console.log(err);
      }
    );
  }

  cancelEdit() {
    this.isEditing = false;
    this.selectedPartyHall = null;
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
