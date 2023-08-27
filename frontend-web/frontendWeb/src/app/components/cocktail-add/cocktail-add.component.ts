import {Component, OnInit} from '@angular/core';
import { Subscription } from 'rxjs';
import { Cocktail } from 'src/app/cocktail';
import { CocktailService } from 'src/app/services/cocktail.service';
import { UiService } from 'src/app/services/ui.service';

@Component({
  selector: 'app-cocktail-add',
  templateUrl: './cocktail-add.component.html',
  styleUrls: ['./cocktail-add.component.css']
})
export class CocktailAddComponent implements OnInit{
  cocktails: Cocktail[] = [];
  showAddCocktail!: boolean;
  subscription!: Subscription;

  constructor(private cocktailService: CocktailService, private uiService: UiService) {
    this.subscription = this.uiService.onToggle().subscribe(value => this.showAddCocktail = value);
  }

  ngOnInit(): void {
    this.cocktailService.getCocktails().subscribe((cocktails) =>
      this.cocktails = cocktails);
  }

  addCocktail(cocktail: Cocktail) {
    this.cocktailService.addCocktail(cocktail).subscribe((cocktail) =>
      this.cocktails.push(cocktail));
  }

  toggleAddPopupbar() {
    this.uiService.toggleAddPopupbar();
  }
}
