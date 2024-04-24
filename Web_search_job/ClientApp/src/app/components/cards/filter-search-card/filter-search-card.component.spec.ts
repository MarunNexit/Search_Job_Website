import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterSearchCardComponent } from './filter-search-card.component';

describe('FilterSearchCardComponent', () => {
  let component: FilterSearchCardComponent;
  let fixture: ComponentFixture<FilterSearchCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FilterSearchCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FilterSearchCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
