import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupbarOverviewComponent } from './popupbar-overview.component';

describe('PopupbarOverviewComponent', () => {
  let component: PopupbarOverviewComponent;
  let fixture: ComponentFixture<PopupbarOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupbarOverviewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupbarOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
