import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployerJobsListComponent } from './employer-jobs-list.component';

describe('EmployerJobsListComponent', () => {
  let component: EmployerJobsListComponent;
  let fixture: ComponentFixture<EmployerJobsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployerJobsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployerJobsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
