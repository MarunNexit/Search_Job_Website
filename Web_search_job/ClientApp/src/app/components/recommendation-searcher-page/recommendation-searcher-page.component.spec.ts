import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecommendationSearcherPageComponent } from './recommendation-searcher-page.component';

describe('RecommendationPageComponent', () => {
  let component: RecommendationSearcherPageComponent;
  let fixture: ComponentFixture<RecommendationSearcherPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecommendationSearcherPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecommendationSearcherPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
