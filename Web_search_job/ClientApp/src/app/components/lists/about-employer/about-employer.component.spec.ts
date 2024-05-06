import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboutEmployerComponent } from './about-employer.component';

describe('AboutEmployerComponent', () => {
  let component: AboutEmployerComponent;
  let fixture: ComponentFixture<AboutEmployerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AboutEmployerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AboutEmployerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
