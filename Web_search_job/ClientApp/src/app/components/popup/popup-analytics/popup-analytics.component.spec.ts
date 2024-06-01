import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupAnalyticsComponent } from './popup-analytics.component';

describe('PopupAnalyticsComponent', () => {
  let component: PopupAnalyticsComponent;
  let fixture: ComponentFixture<PopupAnalyticsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupAnalyticsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupAnalyticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
