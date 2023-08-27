import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { Cocktail } from '../../app/cocktail';
import { CocktailService } from '../../app/services/cocktail.service';

describe('CocktailService', () => {
  let apiURL :string;
  let service: CocktailService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CocktailService],
    });
    apiURL =  'http://localhost:8083/menu/api'
    service = TestBed.inject(CocktailService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get all cocktails', () => {
    const mockCocktails: Cocktail[] = [
      { serialNumber: 1111111111111, name: 'Mojito', purchasePrice: 3.5, category: 'Longdrink', imageUrl: 'mojito.jpg' },
      { serialNumber: 1111111111112, name: 'Caipirinha', purchasePrice: 4.5, category: 'Shortdrink', imageUrl: 'caipirinha.jpg' },
    ];
    service.getCocktails().subscribe((cocktails) => {
      expect(cocktails).toEqual(mockCocktails);
    });
    const req = httpMock.expectOne(`${apiURL}/allCocktails`);
    expect(req.request.method).toBe('GET');
    req.flush(mockCocktails);
  });

  it('should get a specific cocktail', () => {
    const mockCocktail: Cocktail = { serialNumber: 1, name: 'Mojito', purchasePrice: 3.5, category: 'Longdrink', imageUrl: 'mojito.jpg' };
    service.getCocktail(1).subscribe((cocktail) => {
      expect(cocktail).toEqual(mockCocktail);
    });
    const req = httpMock.expectOne(`${apiURL}/cocktail/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockCocktail);
  });

  it('should delete a specific cocktail', () => {
    const mockCocktail: Cocktail = { serialNumber: 1111111111111, name: 'Mojito', purchasePrice: 3.5, category: 'Longdrink', imageUrl: 'mojito.jpg' };
    service.deleteCocktail(mockCocktail).subscribe((cocktail) => {
      expect(cocktail).toEqual(mockCocktail);
    });
    const req = httpMock.expectOne(`${apiURL}/cocktail/1111111111111`);
    expect(req.request.method).toBe('DELETE');
    req.flush(mockCocktail);
  });

  it('should add a new cocktail', () => {
    const mockCocktail: Cocktail = { serialNumber: 1111111111111, name: 'Mojito', purchasePrice: 3.5, category: 'Longdrink', imageUrl: 'mojito.jpg' };
    service.addCocktail(mockCocktail).subscribe((cocktail) => {
      expect(cocktail).toEqual(mockCocktail);
    });
    const req = httpMock.expectOne(`${apiURL}/createCocktail`);
    expect(req.request.method).toBe('POST');
    req.flush(mockCocktail);
  });

  it('should update a cocktail', () => {
    const mockCocktail: Cocktail = { serialNumber: 1111111111111, name: 'Mojito', purchasePrice: 3.5, category: 'Longdrink', imageUrl: 'mojito.jpg' };
    service.updateCocktail(mockCocktail).subscribe((cocktail) => {
      expect(cocktail).toEqual(mockCocktail);
    });
    const req = httpMock.expectOne(`${apiURL}/createCocktail`);
    expect(req.request.method).toBe('POST');
    req.flush(mockCocktail);
  });


});

