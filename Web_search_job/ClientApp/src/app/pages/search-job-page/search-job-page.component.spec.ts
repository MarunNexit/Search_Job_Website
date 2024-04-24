import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchJobPageComponent } from './search-job-page.component';

describe('SearchJobPageComponent', () => {
  let component: SearchJobPageComponent;
  let fixture: ComponentFixture<SearchJobPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchJobPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SearchJobPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
