import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobShortCardComponent } from './job-short-card.component';

describe('JobShortCardComponent', () => {
  let component: JobShortCardComponent;
  let fixture: ComponentFixture<JobShortCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobShortCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobShortCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
