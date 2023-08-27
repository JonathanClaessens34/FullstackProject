import {Component, OnInit} from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Cocktail } from '../cocktail';
import { CocktailService } from '../services/cocktail.service';
import { faTimes, faEdit } from '@fortawesome/free-solid-svg-icons';
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-cocktail-overview',
  templateUrl: './cocktail-overview.component.html',
  styleUrls: ['./cocktail-overview.component.css'],
})
export class CocktailOverviewComponent implements OnInit{
  cocktails: Cocktail[] = [];
  faTimes = faTimes;
  faEdit = faEdit;
  searchQuery = '';

  constructor(private cocktailService: CocktailService, private sanitizer: DomSanitizer, private router: Router) { }

  ngOnInit(): void {
    this.cocktailService.getCocktails().subscribe((cocktails) =>
      this.cocktails = cocktails);
  }

  getImageUrl(url: string) {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

  onDelete(cocktail: Cocktail) {
    this.cocktailService.deleteCocktail(cocktail).subscribe(() =>
      this.cocktails = this.cocktails.filter(t => t.serialNumber !== cocktail.serialNumber));
  }

  onEdit(cocktail: Cocktail) {
    const navigationExtras: NavigationExtras = {
      queryParams: {
        "cocktailSerialNumber": cocktail.serialNumber
      }
    };
    this.router.navigate(['/cocktailedit'], navigationExtras);
  }

  addCocktail(cocktail: Cocktail) {
    this.cocktailService.addCocktail(cocktail).subscribe((cocktail) =>
      this.cocktails.push(cocktail));
  }
}
