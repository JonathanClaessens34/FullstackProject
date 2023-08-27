import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cocktail } from '../cocktail';
import { environment } from '../../../environment';

@Injectable({
  providedIn: 'root'
})
export class CocktailService {
  private apiURL = `${environment.API_URL}/menu/api`;
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Basic ' + btoa('brewer:brewer123')
    })
  };

  constructor(private http: HttpClient) { }

  getCocktails(): Observable<Cocktail[]> {
    return this.http.get<Cocktail[]>(`${this.apiURL}/allCocktails`, this.httpOptions);
  }

  getCocktail(idCocktail: number): Observable<Cocktail> {
    return this.http.get<Cocktail>(`${this.apiURL}/cocktail/${idCocktail}`, this.httpOptions);
  }

  deleteCocktail(cocktail: Cocktail): Observable<Cocktail> {
    const url = `${this.apiURL}/cocktail/${cocktail.serialNumber}`; //Kunnen later beslissen op basis van id
    return this.http.delete<Cocktail>(url, this.httpOptions); //Beslissen of we een confirmation geven bij deleten
  }

  addCocktail(cocktail: Cocktail): Observable<Cocktail> {
    if (cocktail.imageUrl == null) {
      cocktail.imageUrl = undefined;
    }
    const url = `${this.apiURL}/createCocktail`;
    return this.http.post<Cocktail>(url, cocktail, this.httpOptions);
  }

  updateCocktail(cocktail: Cocktail): Observable<Cocktail> {
    const url = `${this.apiURL}/createCocktail`;
    return this.http.post<Cocktail>(url, cocktail, this.httpOptions);
  }
  getapiUrl(){
    return this.apiURL;
  }
}
