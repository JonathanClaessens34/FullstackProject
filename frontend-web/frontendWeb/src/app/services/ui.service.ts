import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UiService {
  private showAddPopupbar = false;
  private subject = new Subject<boolean>();
  private showAddCocktail = false;



  toggleAddPopupbar(): void {
    this.showAddPopupbar = !this.showAddPopupbar;
    this.subject.next(this.showAddPopupbar);
  }

  toggleAddCocktail(): void {
    this.showAddCocktail = !this.showAddCocktail;
    this.subject.next(this.showAddCocktail);
  }

  onToggle(): Observable<boolean> {
    return this.subject.asObservable();
  }
}
