import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { PopupbarService } from '../../services/popupbar.service';
import { Popupbar } from '../../Popupbar';
import { faTimes } from '@fortawesome/free-solid-svg-icons';
import { NavigationExtras, Router } from '@angular/router';
import {Menu} from "../../Menu";
import { CocktailmenuService } from '../../services/cocktailmenu.service';

@Component({
  selector: 'app-popupbars',
  templateUrl: './popupbars.component.html',
  styleUrls: ['./popupbars.component.css']
})
export class PopupbarsComponent implements OnInit {
  popupbars: Popupbar[] = [];

  menu!: Menu;
  faTimes = faTimes;
  @Output() DeletePopupbar: EventEmitter<Popupbar> = new EventEmitter()
  AantalBesteld: number[] = [];

  constructor(private popupbarService: PopupbarService, private cocktailmenuService: CocktailmenuService, private router: Router) {
    this.AantalBesteld = Array(this.popupbars.length).fill(0);
  }

  ngOnInit(): void {
    this.popupbarService.getPopupbars().subscribe((popupbars) =>{
      console.log(popupbars);
      this.popupbars = popupbars;
   /*   this.popupbarIds = this.popupbars.map(popupbar => popupbar.id);



      this.cocktailmenuService.getMenu(this.popupbarIds).subscribe((menu) =>{
        console.log(menu);
        this.menu = menu});*/
      for (let i = 0; i < this.popupbars.length; i++) {
        this.GetAantalBesteld(this.popupbars[i], i);
      }
    });

  }

  deletePopupbar(popupbar: Popupbar) {
    this.popupbarService.deletePopupbar(popupbar).subscribe(() =>
      this.popupbars = this.popupbars.filter(t => t.name !== popupbar.name));
    console.log(popupbar)
  }

  onDelete(popupbar: Popupbar) {
    this.popupbarService.deletePopupbar(popupbar).subscribe(() =>
      this.popupbars = this.popupbars.filter(t => t.id !== popupbar.id));
    console.log("test" + popupbar)
  }

  GetAantalBesteld(popupbar: Popupbar, index: number){
    if (popupbar && popupbar.id) {
      this.cocktailmenuService.getMenu(popupbar.id).subscribe((menu) =>{
        console.log(menu);
        this.menu = menu
        if(menu.orderCocktails && menu.orderCocktails.length > 0){
          this.AantalBesteld[index] = menu.orderCocktails.length;
        }else{
          this.AantalBesteld[index] = 0;
        }
      });
    }
  }





  navigateToCocktailmenu(popupbarId: any) {
    const navigationExtras: NavigationExtras = {
      queryParams: {
        "popupbarId": popupbarId
      }
    };
    this.router.navigate(['/cocktailmenu'], navigationExtras);

  }
}
