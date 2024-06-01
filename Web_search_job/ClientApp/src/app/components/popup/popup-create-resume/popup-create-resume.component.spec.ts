import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupCreateResumeComponent } from './popup-create-resume.component';

describe('PopupCreateResumeComponent', () => {
  let component: PopupCreateResumeComponent;
  let fixture: ComponentFixture<PopupCreateResumeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupCreateResumeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupCreateResumeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
