import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { Popupbar } from '../../app/Popupbar';
import { PopupbarService } from '../../app/services/popupbar.service';

describe('PopupbarService', () => {
  let apiURL :string;
  let service: PopupbarService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PopupbarService]
    });
    apiURL =  'http://localhost:8083/popupbar/api'
    service = TestBed.inject(PopupbarService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get all popupbars', () => {
    const dummyPopupbars: Popupbar[] = [
      {id: 1, name: 'Popupbar 1', location: 'Location 1', brewer: 'Brewer 1'},
      {id: 2, name: 'Popupbar 2', location: 'Location 2', brewer: 'Brewer 2'},
    ];

    service.getPopupbars().subscribe(popupbars => {
      expect(popupbars.length).toBe(2);
      expect(popupbars).toEqual(dummyPopupbars);
    });

    const req = httpMock.expectOne(`${apiURL}/allpopupbars`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyPopupbars);
  });

  it('should delete a popupbar', () => {
    const dummyPopupbar: Popupbar = {id: 1, name: 'Popupbar 1', location: 'Location 1', brewer: 'Brewer 1'};

    service.deletePopupbar(dummyPopupbar).subscribe(popupbar => {
      expect(popupbar).toEqual(dummyPopupbar);
    });

    const req = httpMock.expectOne(`${apiURL}/popupbar/${dummyPopupbar.id}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(dummyPopupbar);
  });

  it('should add a popupbar', () => {
    const dummyPopupbar: Popupbar = {id: 1, name: 'Popupbar 1', location: 'Location 1', brewer: 'Brewer 1'};

    service.addPopupbar(dummyPopupbar).subscribe(popupbar => {
      expect(popupbar).toEqual(dummyPopupbar);
    });

    const req = httpMock.expectOne(`${apiURL}/createpopupbar`);
    expect(req.request.method).toBe('POST');
    req.flush(dummyPopupbar);
  });
});
