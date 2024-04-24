import { Component } from '@angular/core';

@Component({
  selector: 'app-registration-login-page',
  templateUrl: './registration-login-page.component.html',
  styleUrls: ['./registration-login-page.component.scss'],
})
export class RegistrationLoginPageComponent {

  constructor() { }
  ngOnInit(): void {
    const registerButton = document.getElementById("register");
    const loginButton = document.getElementById("login");
    const container = document.getElementById("container_new");

    if (registerButton && loginButton && container) {
      registerButton.addEventListener("click", () => {
        container.classList.add("right-panel-active");
      });

      loginButton.addEventListener("click", () => {
        container.classList.remove("right-panel-active");
      });
    }
  }

}
