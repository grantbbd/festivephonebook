import { Component, OnInit } from '@angular/core';
import { PhonebookService } from '../services/phonebook.service';
import { PhonebookEntryModel } from '../models/phonebookentry.model';
import { Router } from '@angular/router';
import { SharedDataService } from '../services/shared.data.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-phonebook',
  templateUrl: './phonebook.component.html',
  styleUrls: ['./phonebook.component.css']
})
export class PhonebookComponent implements OnInit {
  phonebookEntries: PhonebookEntryModel[] = [];
  phonebookEntriesAll: PhonebookEntryModel[] = [];
  searchText = '';

  constructor(
    private phonebook: PhonebookService,
    private router: Router,
    private sharedDataService: SharedDataService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit() {
    this.spinner.show();
    this.phonebook.getall().subscribe(
      (entries: PhonebookEntryModel[]) => {
        this.phonebookEntries = entries;
        this.phonebookEntriesAll = entries;
        this.spinner.hide();
      },
      error => {
        console.log(error);
        this.spinner.hide();
      }
    );
  }

  CreateEntry(isNice: boolean) {
    this.sharedDataService.isNice = isNice;
    this.EditEntry('');
  }

  EditEntry(id: string) {
    this.sharedDataService.id = id;
    this.router.navigateByUrl('/entry');
  }

  Filter() {
    if (this.searchText) {
      this.phonebookEntries = this.phonebookEntriesAll.filter(
        x =>
          x.firstName.toUpperCase().indexOf(this.searchText.toUpperCase()) >= 0 ||
          x.surname.toUpperCase().indexOf(this.searchText.toUpperCase()) >= 0
      );
    } else {
      this.phonebookEntries = this.phonebookEntriesAll;
    }
  }
}
