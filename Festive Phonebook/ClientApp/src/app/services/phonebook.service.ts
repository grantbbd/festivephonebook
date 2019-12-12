import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import {
  PhonebookEntryModel,
  PhoneBookEntryModelAdapter
} from '../models/phonebookentry.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PhonebookService {
  constructor(
    private http: HttpClient,
    private phoneBookAdapter: PhoneBookEntryModelAdapter
  ) {}

  getall(): Observable<PhonebookEntryModel[]> {
    return this.http
      .get(environment.apiURL + 'phonebook/all')
      .pipe(
        map((data: any[]) =>
          data.map(item => this.phoneBookAdapter.adapt(item))
        )
      );
  }

  get(id: string): Observable<PhonebookEntryModel> {
    return this.http
      .get(environment.apiURL + 'phonebook/get?id=' + id)
      .pipe(map(item => this.phoneBookAdapter.adapt(item)));
  }

  update(entry: PhonebookEntryModel) {
    if (entry.id) {
      return this.http.post(environment.apiURL + 'phonebook/update', entry);
    } else {
      return this.http.post(environment.apiURL + 'phonebook/create', entry);
    }
  }

  delete(id: string) {
    return this.http.get(environment.apiURL + 'phonebook/delete?id=' + id);
  }
}
