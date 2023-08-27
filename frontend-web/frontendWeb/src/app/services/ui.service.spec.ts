import { TestBed } from '@angular/core/testing';
import { UiService } from './ui.service';

describe('UiService', () => {
  let service: UiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call toggleAddPopupbar', () => {
    const spy = jest.spyOn(service, 'toggleAddPopupbar');
    service.toggleAddPopupbar();
    expect(spy).toHaveBeenCalled();
  });

  it('should call toggleAddCocktail', () => {
    const spy = jest.spyOn(service, 'toggleAddCocktail');
    service.toggleAddCocktail();
    expect(spy).toHaveBeenCalled();
  });
});
