import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatRoomsPageComponent } from './chat-rooms-page.component';

describe('ChatRoomsPageComponent', () => {
  let component: ChatRoomsPageComponent;
  let fixture: ComponentFixture<ChatRoomsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChatRoomsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatRoomsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
