import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupProfileSettingsComponent } from './popup-profile-settings.component';

describe('PopupProfileSettingsComponent', () => {
  let component: PopupProfileSettingsComponent;
  let fixture: ComponentFixture<PopupProfileSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupProfileSettingsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupProfileSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
