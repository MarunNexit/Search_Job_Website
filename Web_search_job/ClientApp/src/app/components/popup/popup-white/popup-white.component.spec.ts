import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupWhiteComponent } from './popup-white.component';

describe('PopupWhiteComponent', () => {
  let component: PopupWhiteComponent;
  let fixture: ComponentFixture<PopupWhiteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupWhiteComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupWhiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
