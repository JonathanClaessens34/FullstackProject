import { Component, EventEmitter, Output } from '@angular/core';
import { Subscription } from 'rxjs';
import { Cocktail } from 'src/app/cocktail';
import { UiService } from 'src/app/services/ui.service';

@Component({
  selector: 'app-add-cocktail',
  templateUrl: './add-cocktail.component.html',
  styleUrls: ['./add-cocktail.component.css']
})
export class AddCocktailComponent {
  @Output() AddCocktail: EventEmitter<Cocktail> = new EventEmitter();
  name!: string;
  purchasePrice!: number;
  sellingPrice!: number;
  category!: string;
  imageUrl!: string;
  serialNumber!: number;
  showAddCocktail!: boolean;
  subscription!: Subscription;

  categoryOptions = [
    { value: 0, label: 'AllDay' },
    { value: 1, label: 'BeforeDinner' },
    { value: 2, label: 'AfterDinner' },
    { value: 3, label: 'Stirred' },
    { value: 4, label: 'Sours' },
    { value: 5, label: 'Highballs' },
    { value: 6, label: 'Flips' },
    { value: 7, label: 'Fizzes' },
    { value: 8, label: 'Swizzles' },
    { value: 9, label: 'Smashes' }
  ];

  constructor(private uiService: UiService) {
    this.subscription = this.uiService.onToggle().subscribe(value => this.showAddCocktail = value);
  }


  onSubmit(): void {
    if (!this.serialNumber || !this.name || !this.purchasePrice || !this.category) {
      alert("Gelieve alles in te vullen.");
      return;
    }

    if (this.serialNumber.toString().length != 13) {
      alert("Seriële nummer moet 13 cijfers lang zijn!");
      return;
    }

    const newCocktail = {
      name: this.name,
      purchasePrice: this.purchasePrice,
      category: this.category,
      imageUrl: this.imageUrl,
      serialNumber: +this.serialNumber
    }

    this.AddCocktail.emit(newCocktail);

    this.name = '';
    this.purchasePrice = 0;
    this.category = '';
    this.imageUrl = '';
    this.serialNumber = 0;
  }
}
