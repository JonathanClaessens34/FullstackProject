import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router, ActivatedRoute } from '@angular/router';
import { CocktailService } from '../services/cocktail.service';

@Component({
  selector: 'app-cocktail-edit-fix',
  templateUrl: './cocktail-edit-fix.component.html',
  styleUrls: ['./cocktail-edit-fix.component.css']
})
export class CocktailEditFixComponent {
  // @Output() onAddCocktail: EventEmitter<Cocktail> = new EventEmitter();
  name!: string;
  purchasePrice!: number;
  category!: string;
  imageUrl!: string | undefined;
  serialNumber!: number;
  // cocktail!: Cocktail;
  // subscription!: Subscription;

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

  constructor(private router: Router, private cocktailService: CocktailService, private route: ActivatedRoute, private sanitizer: DomSanitizer) {
    // this.subscription = this.uiService.onToggle().subscribe(value => this.showAddCocktail = value);
    this.route.queryParams.subscribe(params => {
      this.serialNumber = params['cocktailSerialNumber'];
    });
    this.cocktailService.getCocktail(this.serialNumber).subscribe((cocktail) => {
      this.name = cocktail.name;
      this.purchasePrice = cocktail.purchasePrice;
      this.category = cocktail.category;
      this.imageUrl = cocktail.imageUrl;
    })
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

    this.cocktailService.addCocktail(newCocktail).subscribe();

    this.router.navigate(['/cocktail']);
  }
}
