import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimilarJobsListComponent } from './similar-jobs-list.component';

describe('SimilarJobsListComponent', () => {
  let component: SimilarJobsListComponent;
  let fixture: ComponentFixture<SimilarJobsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimilarJobsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SimilarJobsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
