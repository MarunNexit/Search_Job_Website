import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupAddSectionCardComponent } from './popup-add-section-card.component';

describe('PopupAddSectionCardComponent', () => {
  let component: PopupAddSectionCardComponent;
  let fixture: ComponentFixture<PopupAddSectionCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupAddSectionCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupAddSectionCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
