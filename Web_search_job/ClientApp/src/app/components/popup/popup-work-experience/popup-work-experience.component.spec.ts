import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupWorkExperienceComponent } from './popup-work-experience.component';

describe('PopupWorkExperienceComponent', () => {
  let component: PopupWorkExperienceComponent;
  let fixture: ComponentFixture<PopupWorkExperienceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupWorkExperienceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupWorkExperienceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
