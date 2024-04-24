import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationLoginPageComponent } from './registration-login-page.component';

describe('RegistrationLoginPageComponent', () => {
  let component: RegistrationLoginPageComponent;
  let fixture: ComponentFixture<RegistrationLoginPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegistrationLoginPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistrationLoginPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
