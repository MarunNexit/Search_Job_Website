import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopularEmployersCardComponent } from './popular-employers-card.component';

describe('PopularEmployersCardComponent', () => {
  let component: PopularEmployersCardComponent;
  let fixture: ComponentFixture<PopularEmployersCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopularEmployersCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopularEmployersCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
