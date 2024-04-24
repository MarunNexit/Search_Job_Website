import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HighestPayingJobsComponent } from './highest-paying-jobs.component';

describe('HighestPayingJobsComponent', () => {
  let component: HighestPayingJobsComponent;
  let fixture: ComponentFixture<HighestPayingJobsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HighestPayingJobsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HighestPayingJobsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
