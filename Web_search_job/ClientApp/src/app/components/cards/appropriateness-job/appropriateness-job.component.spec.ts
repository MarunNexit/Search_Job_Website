import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppropriatenessJobComponent } from './appropriateness-job.component';

describe('AppropriatenessJobComponent', () => {
  let component: AppropriatenessJobComponent;
  let fixture: ComponentFixture<AppropriatenessJobComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppropriatenessJobComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AppropriatenessJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
