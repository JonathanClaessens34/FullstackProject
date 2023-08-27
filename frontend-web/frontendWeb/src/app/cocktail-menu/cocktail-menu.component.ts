import {Component, OnInit} from '@angular/core';
import { faTimes } from '@fortawesome/free-solid-svg-icons';
import { DomSanitizer } from '@angular/platform-browser';
import { CocktailmenuService } from '../services/cocktailmenu.service';
import { ActivatedRoute } from '@angular/router';
import { Menu } from '../Menu';
import { CocktailMenuItemRequest } from '../cocktailmenuitemrequest';

@Component({
  selector: 'app-cocktail-menu',
  templateUrl: './cocktail-menu.component.html',
  styleUrls: ['./cocktail-menu.component.css']
})
export class CocktailMenuComponent implements OnInit{
  menu!: Menu;
  faTimes = faTimes;
  searchQuery = '';
  popupbarId: number;

  constructor(private cocktailmenuService: CocktailmenuService, private sanitizer: DomSanitizer, private route: ActivatedRoute) {
    this.popupbarId = 0;
    this.route.queryParams.subscribe(params => {
      this.popupbarId = params['popupbarId'];
    });
  }

  ngOnInit(): void {
    this.cocktailmenuService.getMenu(this.popupbarId).subscribe((menu) => {
      console.log(menu);
      this.menu = menu
    });
  }

  getImageUrl(url: string) {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

   onDelete(serialNumber: number) {
    if(serialNumber){
      const body = { serialNumber: serialNumber };
      this.cocktailmenuService.deleteCocktail(this.menu.id, body).subscribe(() => {
        this.menu.cocktails = this.menu.cocktails.filter(cocktailMenuItem => cocktailMenuItem.cocktail.serialNumber !== serialNumber);
        console.log(serialNumber)
      });
      console.log(serialNumber)
    }
   }


  addCocktail(cocktailMenuItem: CocktailMenuItemRequest) {
    this.cocktailmenuService.addCocktail(cocktailMenuItem, this.menu.id).subscribe((cocktail) => {
      console.log(cocktail);
      this.menu = cocktail;
    }
    );
    // this.menu.cocktails.push(cocktail));
  }
}
