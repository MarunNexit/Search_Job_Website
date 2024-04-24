import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderSearcherComponent } from './header-searcher.component';

describe('HeaderSearcherComponent', () => {
  let component: HeaderSearcherComponent;
  let fixture: ComponentFixture<HeaderSearcherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HeaderSearcherComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HeaderSearcherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
