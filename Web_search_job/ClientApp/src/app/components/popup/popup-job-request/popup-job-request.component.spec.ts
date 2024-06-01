import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupJobRequestComponent } from './popup-job-request.component';

describe('PopupJobRequestComponent', () => {
  let component: PopupJobRequestComponent;
  let fixture: ComponentFixture<PopupJobRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopupJobRequestComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupJobRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
