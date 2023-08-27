import { Cocktail } from "./cocktail";

export interface CocktailMenuItem {
    id?: number;
    cocktail: Cocktail;
    sellingPrice: number;
}
