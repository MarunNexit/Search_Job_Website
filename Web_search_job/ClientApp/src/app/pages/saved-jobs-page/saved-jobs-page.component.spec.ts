import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SavedJobsPageComponent } from './saved-jobs-page.component';

describe('SavedJobsPageComponent', () => {
  let component: SavedJobsPageComponent;
  let fixture: ComponentFixture<SavedJobsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SavedJobsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SavedJobsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
