import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopularEmployersPageComponent } from './popular-employers-page.component';

describe('PopularEmployersPageComponent', () => {
  let component: PopularEmployersPageComponent;
  let fixture: ComponentFixture<PopularEmployersPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopularEmployersPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopularEmployersPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
