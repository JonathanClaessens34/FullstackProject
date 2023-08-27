import {Component, Input, OnInit} from '@angular/core';
import { Subscription } from 'rxjs';
import { Menu } from 'src/app/Menu';
import { CocktailMenuItemRequest } from 'src/app/cocktailmenuitemrequest';
import { CocktailmenuService } from 'src/app/services/cocktailmenu.service';
import { UiService } from 'src/app/services/ui.service';

@Component({
  selector: 'app-cocktail-menu-add',
  templateUrl: './cocktail-menu-add.component.html',
  styleUrls: ['./cocktail-menu-add.component.css']
})
export class CocktailMenuAddComponent implements OnInit{
  menu!: Menu;
  showAddCocktail!: boolean;
  subscription!: Subscription;
  @Input() menuId!: any;

  constructor(private cocktailmenuService: CocktailmenuService, private uiService: UiService) {
    this.subscription = this.uiService.onToggle().subscribe(value => this.showAddCocktail = value);
  }

  ngOnInit(): void {
    this.cocktailmenuService.getMenu(this.menuId).subscribe((menu) =>
      this.menu = menu);
  }

  addCocktail(cocktail: CocktailMenuItemRequest) {
    this.cocktailmenuService.addCocktail(cocktail, this.menuId).subscribe((menu) => {
      console.log(menu);
      // console.log(menu.cocktails[menu.cocktails.length - 1]);
      this.menu.cocktails.push(menu.cocktails[menu.cocktails.length - 1]);
    }
      // this.menu.cocktails = cocktail.cocktails
    );
  }

  toggleAddPopupbar() {
    this.uiService.toggleAddPopupbar();
  }
}
