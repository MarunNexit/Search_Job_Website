import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlankBlockNoResultComponent } from './blank-block-no-result.component';

describe('BlankBlockNoResultComponent', () => {
  let component: BlankBlockNoResultComponent;
  let fixture: ComponentFixture<BlankBlockNoResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BlankBlockNoResultComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BlankBlockNoResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
