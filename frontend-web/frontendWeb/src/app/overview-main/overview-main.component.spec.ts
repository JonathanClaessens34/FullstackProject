import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OverviewMainComponent } from './overview-main.component';

describe('OverviewMainComponent', () => {
  let component: OverviewMainComponent;
  let fixture: ComponentFixture<OverviewMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OverviewMainComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OverviewMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
