import { Component, OnInit } from '@angular/core';
import { SeasonalVacationService } from 'src/app/services/seasonal-vacation.service';
import { SeasonalVacation } from 'src/app/models/vacationModel/seasonal-vacation.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthGuardService } from 'src/app/services/auth-guard.service';



@Component({
  selector: 'app-vacations',
  templateUrl: './vacations.component.html',
  styleUrls: ['./vacations.component.css'],
})
export class VacationsComponent implements OnInit {
  seasonalVacations: SeasonalVacation[] = [];
  vacationForm: FormGroup; // Create a FormGroup
  editForm: FormGroup; // Create an edit FormGroup
  newVacation: SeasonalVacation = { id: 0, name: '', vacationDate: new Date() }; // Initialize with empty data
  selectedVacation: SeasonalVacation | null = null;





  constructor(private vacationService: SeasonalVacationService, private authGuard: AuthGuardService) {
    // Initialize the vacationForm property here
    this.vacationForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      vacationDate: new FormControl('', [Validators.required]),
    });



    // Initialize the editForm property here
    this.editForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      vacationDate: new FormControl('', [Validators.required]),
    });
  }

  token = localStorage.getItem("jwt") ?? "";

  hasPermissions(permissions: string[]){
    return this.authGuard.hasPermission(permissions) || this.authGuard.hasRole(['HumanResource']);
  }

  ngOnInit(): void {
    this.loadSeasonalVacations();
  }

  loadSeasonalVacations() {
    this.vacationService.getSeasonalVacations().subscribe({
      next: (data) => {
        this.seasonalVacations = data;
      },
      error:(error) => {
        console.error('Error loading seasonal vacations:', error);
      },
      complete:()=>{}
  });
  }
  // validation for add new vacation
  get name() {
    return this.vacationForm.controls['name'];
  }

  get vacationDate() {
    return this.vacationForm.controls['vacationDate'];
  }


  //validation for save edit
  get nameEdited() {
    return this.editForm.controls['name'];
  }

  get vacationDateEdited() {
    return this.editForm.controls['vacationDate'];
  }
  onSubmit() {
    this.newVacation.name = this.vacationForm.get('name')?.value;
    this.newVacation.vacationDate = this.vacationForm.get('vacationDate')?.value;

    this.vacationService.createSeasonalVacation(this.newVacation).subscribe({
      next: (data) => {

        this.loadSeasonalVacations(); // Reload the list after adding a new vacation
        this.newVacation = { id: 0, name: '', vacationDate: new Date() }; // Clear the form
        this.vacationForm.reset();
      },
      error: (error) => {
        console.error('Error creating seasonal vacation:', error);
      },
    });
  }

// Function to select a vacation for editing
editVacation(vacation: SeasonalVacation) {
  this.selectedVacation = vacation;

  // Populate the edit form with the selected vacation's data
  this.editForm.patchValue({
    name: vacation.name,
    vacationDate: new Date(vacation.vacationDate).toISOString().substring(0, 10),
  });
}

// Function to save the edited vacation
saveEditedVacation() {
  if (this.selectedVacation) {
    const editedVacation: SeasonalVacation = {
      ...this.selectedVacation,
      name: this.editForm.get('name')?.value,
      vacationDate: new Date(this.editForm.get('vacationDate')?.value),
    };

    this.vacationService.updateSeasonalVacation(editedVacation).subscribe(
      (data) => {
        // Clear the selected vacation
        this.selectedVacation = null;
        // Reload the list after editing the vacation
        this.loadSeasonalVacations();
      },
      (error) => {
        console.error('Error updating seasonal vacation:', error);
      }
    );
  }
}


  // Variable to store the selected vacation for deletion
  vacationToDelete: SeasonalVacation | null = null;

  deleteVacation(vacation: SeasonalVacation) {
    this.vacationService.deleteSeasonalVacation(vacation.id!).subscribe(
      () => {
        // Use the filter method to remove the vacation from the array
        this.seasonalVacations = this.seasonalVacations.filter((v) => v.id !== vacation.id);
      },
      (error) => {
        console.error('Error deleting seasonal vacation:', error);
      }
    );
  }
}
