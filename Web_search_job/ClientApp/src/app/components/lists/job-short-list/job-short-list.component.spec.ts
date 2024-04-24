import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobShortListComponent } from './job-short-list.component';

describe('JobShortListComponent', () => {
  let component: JobShortListComponent;
  let fixture: ComponentFixture<JobShortListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobShortListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobShortListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
