import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChipJobListComponent } from './chip-job-list.component';

describe('ChipJobComponent', () => {
  let component: ChipJobListComponent;
  let fixture: ComponentFixture<ChipJobListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChipJobListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChipJobListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
