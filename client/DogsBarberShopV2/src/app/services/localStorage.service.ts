import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export default class LocalStorageService {
  getItem(key: string): string | null {
    return localStorage.getItem(key);
  }

  addItem(key: string, item: string): void {
    localStorage.setItem(key, item);
  }

  removeItem(key: string): void {
    localStorage.removeItem(key);
  }

  clearStorage(): void {
    localStorage.clear();
  }
}
