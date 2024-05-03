import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChipsJobPriorityComponent } from './chips-job-priority.component';

describe('ChipsJobPriorityComponent', () => {
  let component: ChipsJobPriorityComponent;
  let fixture: ComponentFixture<ChipsJobPriorityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChipsJobPriorityComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChipsJobPriorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
