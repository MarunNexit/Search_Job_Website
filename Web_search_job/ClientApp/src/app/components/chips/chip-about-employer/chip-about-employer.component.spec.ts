import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChipAboutEmployerComponent } from './chip-about-employer.component';

describe('ChipAboutEmployerComponent', () => {
  let component: ChipAboutEmployerComponent;
  let fixture: ComponentFixture<ChipAboutEmployerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChipAboutEmployerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChipAboutEmployerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
