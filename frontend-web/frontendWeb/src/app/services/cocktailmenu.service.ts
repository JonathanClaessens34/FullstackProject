import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cocktail } from '../cocktail';
import { Menu } from '../Menu';
import { CocktailMenuItemRequest } from '../cocktailmenuitemrequest';
import { environment } from '../../../environment';

@Injectable({
  providedIn: 'root'
})
export class CocktailmenuService {
  private apiURL = `${environment.API_URL}/menu/api`;
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Basic ' + btoa('brewer:brewer123')
    })
  };

  constructor(private http: HttpClient) { }

  getMenu(popupbarId: number): Observable<Menu> {
    return this.http.get<Menu>(`${this.apiURL}/menu/popupbar/${popupbarId}`, this.httpOptions);
  }

  deleteCocktail(menuId: number | undefined, body: { serialNumber: number }): Observable<Cocktail> {
    const url = `${this.apiURL}/removeCocktailFromMenu/${menuId}`;
    return this.http.put<Cocktail>(url, body, this.httpOptions);
  }

  addCocktail(cocktailMenuItem: CocktailMenuItemRequest, menuId: number | undefined): Observable<Menu> {
    const url = `${this.apiURL}/addCocktailToMenu/${menuId}`;
    return this.http.put<Menu>(url, cocktailMenuItem, this.httpOptions);
  }
}
