import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FooterSearcherComponent } from './footer-searcher.component';

describe('FooterSearcherComponent', () => {
  let component: FooterSearcherComponent;
  let fixture: ComponentFixture<FooterSearcherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FooterSearcherComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FooterSearcherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
