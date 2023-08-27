import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Popupbar } from '../Popupbar';
import { Observable } from 'rxjs';
import { environment } from '../../../environment';

@Injectable({
  providedIn: 'root'
})
export class PopupbarService {
  private apiURL = `${environment.API_URL}/popupbar/api`;
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Basic ' + btoa('brewer:brewer123')
    })
  };

  constructor(private http: HttpClient) { }

  getPopupbars(): Observable<Popupbar[]> {
    return this.http.get<Popupbar[]>(`${this.apiURL}/allpopupbars`, this.httpOptions);
  }

  deletePopupbar(popUpBar: Popupbar): Observable<Popupbar> {
    const url = `${this.apiURL}/popupbar/${popUpBar.id}`; //Kunnen later beslissen op basis van id
    return this.http.delete<Popupbar>(url, this.httpOptions); //Beslissen of we een confirmation geven bij deleten
  }

  addPopupbar(popUpBar: Popupbar): Observable<Popupbar> {
    const url = `${this.apiURL}/createpopupbar`;
    return this.http.post<Popupbar>(url, popUpBar, this.httpOptions);
  }
}
