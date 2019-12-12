import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { PhonebookService } from '../services/phonebook.service';
import { PhonebookEntryModel } from '../models/phonebookentry.model';
import { Router } from '@angular/router';
import { SharedDataService } from '../services/shared.data.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-phonebook-entry',
  templateUrl: './phonebook-entry.component.html',
  styleUrls: ['./phonebook-entry.component.css']
})
export class PhonebookEntryComponent implements OnInit {
  isNice = true;
  errors: string[] = [];

  entryForm = this.formBuilder.group({
    firstName: ['', Validators.required],
    surname: ['', Validators.required],
    telephoneNumber: ['', Validators.required]
  });

  constructor(
    private formBuilder: FormBuilder,
    private phonebookService: PhonebookService,
    private router: Router,
    public sharedDataService: SharedDataService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit() {
    this.isNice = this.sharedDataService.isNice;
    if (this.sharedDataService.id) {
      this.spinner.show();
      this.phonebookService.get(this.sharedDataService.id).subscribe(
        (res: PhonebookEntryModel) => {
          this.entryForm.patchValue({
            firstName: res.firstName,
            surname: res.surname,
            telephoneNumber: res.phoneNumber
          });
          this.isNice = res.kind === 1;
          this.spinner.hide();
        },
        err => {
          console.log(err);
          this.spinner.hide();
        }
      );
    }
  }

  OnSubmit() {
    this.errors = [];
    this.spinner.show();
    this.phonebookService
      .update(
        new PhonebookEntryModel(
          this.sharedDataService.id,
          '',
          this.isNice ? 1 : 2,
          this.entryForm.get('telephoneNumber').value,
          this.entryForm.get('firstName').value,
          this.entryForm.get('surname').value
        )
      )
      .subscribe(
        res => {
          this.spinner.hide();
          this.router.navigateByUrl('/phonebook');
        },
        err => {
          this.spinner.hide();
          this.errors.push('A technical error occurred saving the entry.');
        }
      );
  }

  OnCancel() {
    this.router.navigateByUrl('/phonebook');
  }

  DeleteEntry() {
    this.spinner.show();
    this.phonebookService.delete(this.sharedDataService.id).subscribe(
      res => {
        this.spinner.hide();
        this.router.navigateByUrl('/phonebook');
      },
      err => {
        this.errors.push(
          'A technical error occurred trying to delete the entry.'
        );
        this.spinner.hide();
      }
    );
  }
}
