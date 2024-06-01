import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupUserInfoComponent } from './popup-user-info.component';

describe('PopupUserInfoComponent', () => {
  let component: PopupUserInfoComponent;
  let fixture: ComponentFixture<PopupUserInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupUserInfoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupUserInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
