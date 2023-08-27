import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { Cocktail } from '../../app/cocktail';
import { Menu } from '../../app/Menu';
import { CocktailMenuItemRequest } from '../../app/cocktailmenuitemrequest';
import { CocktailmenuService } from '../../app/services/cocktailmenu.service';

describe('CocktailmenuService', () => {
  let apiURL :string;
  let service: CocktailmenuService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CocktailmenuService]
    });
    apiURL =  'http://localhost:8083/menu/api'
    service = TestBed.inject(CocktailmenuService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get menu for a specific popupbar', () => {
    const dummyMenu: Menu = {
      id: 1,
      popUpBarId: 1,
      cocktails: [
        {id: 1, cocktail: { serialNumber: 1, name: 'Cocktail 1', purchasePrice: 5, category: 'Category 1', imageUrl: 'image1' }, sellingPrice: 7},
        {id: 2, cocktail: { serialNumber: 2, name: 'Cocktail 2', purchasePrice: 6, category: 'Category 2', imageUrl: 'image2' }, sellingPrice: 8},
      ],
      orderCocktails: [
        { serialNumber: 1, name: 'Cocktail 1', purchasePrice: 5, category: 'Category 1', imageUrl: 'image1' },
        { serialNumber: 2, name: 'Cocktail 2', purchasePrice: 6, category: 'Category 2', imageUrl: 'image2' }
      ]
    };

    service.getMenu(1).subscribe(menu => {
      expect(menu).toEqual(dummyMenu);
    });

    const req = httpMock.expectOne(`${apiURL}/menu/popupbar/1`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyMenu);
  });

  it('should delete a cocktail from menu', () => {
    const body = {
      serialNumber: 1,
    }
    const dummyCocktail: Cocktail = {
      serialNumber: 1,
      name: 'Cocktail 1',
      purchasePrice: 5,
      category: 'Category 1',
      imageUrl: 'image1'
    };
    service.deleteCocktail(1, body).subscribe(cocktail => {
      expect(cocktail).toEqual(dummyCocktail);
    });

    const req = httpMock.expectOne(`${apiURL}/removeCocktailFromMenu/1`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(body);
    req.flush(dummyCocktail);
  });

  it('should add a cocktail to menu', () => {
    const dummyCocktailMenuItemRequest: CocktailMenuItemRequest = {
      serialNumber: 1,
      price: 7
    }
    const dummyMenu: Menu = {
      id: 1,
      popUpBarId: 1,
      cocktails: [
        {id: 1, cocktail: { serialNumber: 1, name: 'Cocktail 1', purchasePrice: 5, category: 'Category 1', imageUrl: 'image1' }, sellingPrice: 7},
        {id: 2, cocktail: { serialNumber: 2, name: 'Cocktail 2', purchasePrice: 6, category: 'Category 2', imageUrl: 'image2' }, sellingPrice: 8},
      ],
      orderCocktails: [
        { serialNumber: 1, name: 'Cocktail 1', purchasePrice: 5, category: 'Category 1', imageUrl: 'image1' },
        { serialNumber: 2, name: 'Cocktail 2', purchasePrice: 6, category: 'Category 2', imageUrl: 'image2' }
      ]
    };

    service.addCocktail(dummyCocktailMenuItemRequest, 1).subscribe(menu => {
      expect(menu).toEqual(dummyMenu);
    });

    const req = httpMock.expectOne(`${apiURL}/addCocktailToMenu/1`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(dummyCocktailMenuItemRequest);
    req.flush(dummyMenu);
  });
});

