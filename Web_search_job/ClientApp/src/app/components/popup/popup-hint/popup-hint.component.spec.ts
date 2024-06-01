import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupHintComponent } from './popup-hint.component';

describe('PopupHintComponent', () => {
  let component: PopupHintComponent;
  let fixture: ComponentFixture<PopupHintComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupHintComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupHintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
