import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobCreatePageComponent } from './job-create-page.component';

describe('JobCreatePageComponent', () => {
  let component: JobCreatePageComponent;
  let fixture: ComponentFixture<JobCreatePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobCreatePageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobCreatePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
