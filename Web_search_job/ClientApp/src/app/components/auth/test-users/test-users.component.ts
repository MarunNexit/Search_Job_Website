import { Component } from '@angular/core';
import { AuthTestingClient } from '../../../models/backend/dtos/auth/auth-testing-client';

@Component({
  selector: 'app-test-users',
  templateUrl: './test-users.component.html',
  styleUrls: ['./test-users.component.scss'],
})
export class TestUsersComponent {
  public textForAll = '';
  public textForRegistered = '';
  public textForAdmins = '';

  constructor(private authTestingClient: AuthTestingClient) {}

  public getForAll() {
    this.authTestingClient
      .getTextForAll()
      .subscribe(text => (this.textForAll = text));
  }

  public getForRegistered() {
    this.authTestingClient.getTextForRegistered().subscribe(text => {
      this.textForRegistered = text;
    });
  }

  public getForAdmins() {
    this.authTestingClient
      .getTextForAdmins()
      .subscribe(text => (this.textForAdmins = text));
  }
}
