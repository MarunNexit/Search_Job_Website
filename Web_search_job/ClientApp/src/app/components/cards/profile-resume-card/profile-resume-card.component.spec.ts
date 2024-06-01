import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileResumeCardComponent } from './profile-resume-card.component';

describe('ProfileResumeCardComponent', () => {
  let component: ProfileResumeCardComponent;
  let fixture: ComponentFixture<ProfileResumeCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileResumeCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileResumeCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
