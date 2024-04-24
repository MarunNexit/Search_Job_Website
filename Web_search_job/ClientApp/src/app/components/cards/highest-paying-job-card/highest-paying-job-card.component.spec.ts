import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HighestPayingJobCardComponent } from './highest-paying-job-card.component';

describe('HighestPayingJobCardComponent', () => {
  let component: HighestPayingJobCardComponent;
  let fixture: ComponentFixture<HighestPayingJobCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HighestPayingJobCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HighestPayingJobCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
