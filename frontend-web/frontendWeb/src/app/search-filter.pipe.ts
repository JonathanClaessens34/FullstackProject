import { Pipe, PipeTransform } from '@angular/core';
import {Cocktail} from "./cocktail";

@Pipe({
  name: 'searchFilter'
})
export class SearchFilterPipe implements PipeTransform {

  transform(items: Cocktail[], searchText: string): Cocktail[] {
    console.log('Input:', items, searchText);
    if (!items) { return []; }
    if (!searchText) { return items; }

    searchText = searchText.toLowerCase();

/*    return items.filter(item => {
      return Object.values(item).some((val: any) => val.toString().toLowerCase().includes(searchText));
    });*/
    const output = items.filter(item => {
      return Object.values(item).some((val: string | number) => {
        if (val == null) {
          return false;
        }
        return val.toString().toLowerCase().includes(searchText);
      });
    });
    console.log('Output:', output);
    return output;
  }


}
