import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  saveItem(key: string, item: any): void {
    item = typeof item !== 'string' ? item.toString() : item;
    localStorage.setItem(key,item);
  }

  getItem(key: string): string | null {
    return localStorage.getItem(key);
  }

  deleteItem(key: string): void {
    localStorage.removeItem(key);
  }
}
