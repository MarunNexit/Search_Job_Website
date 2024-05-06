import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubscribeToEmployerComponent } from './subscribe-to-employer.component';

describe('SubscribeToEmployerComponent', () => {
  let component: SubscribeToEmployerComponent;
  let fixture: ComponentFixture<SubscribeToEmployerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubscribeToEmployerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubscribeToEmployerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
