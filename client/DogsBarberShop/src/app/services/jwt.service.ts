import { Injectable } from '@angular/core';
import jwtDecode from 'jwt-decode';
import { User } from '../models/User';

type DecodedData = {
  userName: string;
  firstName: string;
  exp: number;
};

@Injectable({
  providedIn: 'root',
})
export class JwtService {
  createUserFromToken(jwtToken: string): User | null {
    try {
      const decodedData: DecodedData = jwtDecode(jwtToken);
      const isValid = this.validateDecodedData(decodedData);
      if (!isValid) return null;

      return new User(decodedData.userName, decodedData.firstName, jwtToken);
    } catch (error) {
      return null;
    }
  }

  private validateDecodedData(decodedData: DecodedData): boolean {
    if (!decodedData.userName || !decodedData.firstName || !decodedData.exp)
      return false;

    return decodedData.exp * 1000 > Date.now();
  }
}
