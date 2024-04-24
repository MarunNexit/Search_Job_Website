import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChipJobComponent } from './chip-job.component';

describe('ChipJobComponent', () => {
  let component: ChipJobComponent;
  let fixture: ComponentFixture<ChipJobComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChipJobComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChipJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
