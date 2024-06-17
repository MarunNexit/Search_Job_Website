import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupAddUserInfoComponent } from './popup-add-user-info.component';

describe('PopupAddUserInfoComponent', () => {
  let component: PopupAddUserInfoComponent;
  let fixture: ComponentFixture<PopupAddUserInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupAddUserInfoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupAddUserInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
