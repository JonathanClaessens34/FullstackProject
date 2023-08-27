import {Cocktail} from "./cocktail";
import { CocktailMenuItem } from "./cocktailmenu";

export interface Menu {
  id?: number;
  popUpBarId: number;
  cocktails: CocktailMenuItem[];
  orderCocktails: Cocktail[];
}
