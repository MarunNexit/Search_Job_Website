import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobRequestChipComponent } from './job-request-chip.component';

describe('JobRequestChipComponent', () => {
  let component: JobRequestChipComponent;
  let fixture: ComponentFixture<JobRequestChipComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobRequestChipComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobRequestChipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
