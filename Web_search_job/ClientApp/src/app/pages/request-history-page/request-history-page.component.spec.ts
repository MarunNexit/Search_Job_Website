import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestHistoryPageComponent } from './request-history-page.component';

describe('RequestHistoryPageComponent', () => {
  let component: RequestHistoryPageComponent;
  let fixture: ComponentFixture<RequestHistoryPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestHistoryPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RequestHistoryPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
