import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupAddSectionComponent } from './popup-add-section.component';

describe('PopupAddSectionComponent', () => {
  let component: PopupAddSectionComponent;
  let fixture: ComponentFixture<PopupAddSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupAddSectionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupAddSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
