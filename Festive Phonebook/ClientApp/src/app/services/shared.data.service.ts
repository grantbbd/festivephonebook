import { Injectable } from '@angular/core';
import { identifierModuleUrl } from '@angular/compiler';

@Injectable( {
    providedIn: 'root'
})
export class SharedDataService {

    public id = '';
    public username = '';
    public isNice = true;
}