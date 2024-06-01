import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupPurpleComponent } from './popup-purple.component';

describe('PopupPurpleComponent', () => {
  let component: PopupPurpleComponent;
  let fixture: ComponentFixture<PopupPurpleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupPurpleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupPurpleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
