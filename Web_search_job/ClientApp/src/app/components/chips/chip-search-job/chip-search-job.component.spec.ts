import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChipSearchJobComponent } from './chip-search-job.component';

describe('ChipSearchJobComponent', () => {
  let component: ChipSearchJobComponent;
  let fixture: ComponentFixture<ChipSearchJobComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChipSearchJobComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChipSearchJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
