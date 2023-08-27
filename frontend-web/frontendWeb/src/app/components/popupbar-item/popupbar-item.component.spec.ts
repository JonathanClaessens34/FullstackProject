import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupbarItemComponent } from './popupbar-item.component';

describe('PopupbarItemComponent', () => {
  let component: PopupbarItemComponent;
  let fixture: ComponentFixture<PopupbarItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupbarItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupbarItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
