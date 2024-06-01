import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupPrivacyComponent } from './popup-privacy.component';

describe('PopupPrivacyComponent', () => {
  let component: PopupPrivacyComponent;
  let fixture: ComponentFixture<PopupPrivacyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupPrivacyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupPrivacyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
