import { SearchFilterPipe } from '../app/search-filter.pipe';
import {Cocktail} from "../app/cocktail";

describe('SearchFilterPipe', () => {
  let pipe: SearchFilterPipe;
  let items: Cocktail[];

  beforeEach(() => {
    pipe = new SearchFilterPipe();
    items = [
      { serialNumber: 1111111111111, name: 'Mojito', purchasePrice: 3.5, category: 'Longdrink', imageUrl: 'mojito.jpg' },
      { serialNumber: 1111111111112, name: 'Caipirinha', purchasePrice: 4.5, category: 'Shortdrink', imageUrl: 'caipirinha.jpg' },
      { serialNumber: 1111111111113, name: 'Daiquiri', purchasePrice: 3.5, category: 'Longdrink', imageUrl: 'Daiquiri.jpg' }
    ];
  });

  it('should return the original list of items if search text is empty', () => {
    const result = pipe.transform(items, '');
    expect(result).toEqual(items);
  });

  it('should filter the items based on the search text', () => {
    const result = pipe.transform(items, 'Longdrink');
    expect(result).toEqual([
      { serialNumber: 1111111111111, name: 'Mojito', purchasePrice: 3.5, category: 'Longdrink', imageUrl: 'mojito.jpg' },
      { serialNumber: 1111111111113, name: 'Daiquiri', purchasePrice: 3.5, category: 'Longdrink', imageUrl: 'Daiquiri.jpg' }
    ]);
  });

  it('should be case-insensitive', () => {
    const result = pipe.transform(items, 'MOJITO');
    expect(result).toEqual([
      { serialNumber: 1111111111111, name: 'Mojito', purchasePrice: 3.5, category: 'Longdrink', imageUrl: 'mojito.jpg' }
    ]);
  });
});
