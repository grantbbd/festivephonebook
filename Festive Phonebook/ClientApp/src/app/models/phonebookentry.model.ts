import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class PhonebookEntryModel {
  constructor(
    public id: string,
    public userId: string,
    public kind: number,
    public phoneNumber: string,
    public firstName: string,
    public surname: string
  ) {}
}

@Injectable({ providedIn: 'root' })
export class PhoneBookEntryModelAdapter
  implements Adapter<PhonebookEntryModel> {
  adapt(item: any): PhonebookEntryModel {
    return new PhonebookEntryModel(
      item.id,
      item.userId,
      item.kind,
      item.phoneNumber,
      item.firstName,
      item.surname
    );
  }
}
