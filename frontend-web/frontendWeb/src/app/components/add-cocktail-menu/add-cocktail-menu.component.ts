import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import { Subscription } from 'rxjs';
import { Cocktail } from 'src/app/cocktail';
import { CocktailService } from '../../services/cocktail.service';
import { CocktailMenuItemRequest } from 'src/app/cocktailmenuitemrequest';
import { UiService } from 'src/app/services/ui.service';

@Component({
  selector: 'app-add-cocktail-menu',
  templateUrl: './add-cocktail-menu.component.html',
  styleUrls: ['./add-cocktail-menu.component.css']
})
export class AddCocktailMenuComponent implements OnInit{
  @Output() AddCocktail: EventEmitter<CocktailMenuItemRequest> = new EventEmitter();
  cocktailId!: number;
  price!: number;
  showAddCocktail!: boolean;
  subscription!: Subscription;
  cocktails: Cocktail[] =[];

  constructor(private cocktailService: CocktailService, private uiService: UiService) {
    this.subscription = this.uiService.onToggle().subscribe(value => this.showAddCocktail = value);
  }

  ngOnInit(): void {
    this.cocktailService.getCocktails().subscribe((cocktails) =>
      this.cocktails = cocktails);
  }

  onSubmit(): void {
    if (!this.cocktailId || !this.price) {
      alert("Please enter all data!");
      return;
    }

    const newCocktail = {
      serialNumber: +this.cocktailId,
      price: +this.price
    }

    this.AddCocktail.emit(newCocktail);

    this.cocktailId = 0;
    this.price = 0;
  }
}
