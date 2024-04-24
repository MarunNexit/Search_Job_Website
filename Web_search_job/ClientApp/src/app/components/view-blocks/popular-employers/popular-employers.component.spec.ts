import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopularEmployersComponent } from './popular-employers.component';

describe('PopularEmployersComponent', () => {
  let component: PopularEmployersComponent;
  let fixture: ComponentFixture<PopularEmployersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopularEmployersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopularEmployersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
