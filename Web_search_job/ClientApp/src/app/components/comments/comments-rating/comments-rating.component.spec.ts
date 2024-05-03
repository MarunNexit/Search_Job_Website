import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommentsRatingComponent } from './comments-rating.component';

describe('CommentsRatingComponent', () => {
  let component: CommentsRatingComponent;
  let fixture: ComponentFixture<CommentsRatingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CommentsRatingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommentsRatingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
