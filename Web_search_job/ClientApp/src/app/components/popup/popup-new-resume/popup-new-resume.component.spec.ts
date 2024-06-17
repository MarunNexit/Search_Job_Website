import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupNewResumeComponent } from './popup-new-resume.component';

describe('PopupNewResumeComponent', () => {
  let component: PopupNewResumeComponent;
  let fixture: ComponentFixture<PopupNewResumeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupNewResumeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupNewResumeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
