import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SeasonalVacation } from '../models/vacationModel/seasonal-vacation.model';

@Injectable({
  providedIn: 'root',
})
export class SeasonalVacationService {
  private apiUrl = 'https://localhost:7073/api';

  constructor(private http: HttpClient) {}

  get tokenGetter() {
    return localStorage.getItem("jwt");
  }

  getSeasonalVacations(): Observable<SeasonalVacation[]> {
    return this.http.get<SeasonalVacation[]>("https://localhost:7073/api/SeasonalVacation",{
      headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
    });
  }

  createSeasonalVacation(
    seasonalVacation: SeasonalVacation
  ): Observable<SeasonalVacation> {
    return this.http.post<SeasonalVacation>(
      "https://localhost:7073/api/SeasonalVacation",
      seasonalVacation
      ,{
        headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
      });
  }

  updateSeasonalVacation(
    seasonalVacation: SeasonalVacation
  ): Observable<SeasonalVacation> {
    return this.http.put<SeasonalVacation>(
      `${this.apiUrl}/seasonalvacation/${seasonalVacation.id}`,
      seasonalVacation,{
        headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
      }
    );
  }

  deleteSeasonalVacation(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/seasonalvacation/${id}`,{
      headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
    });
  }
}
