import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-registration-login-page',
  templateUrl: './registration-login-page.component.html',
  styleUrls: ['./registration-login-page.component.scss'],
})
export class RegistrationLoginPageComponent implements OnInit {

  IsPasswordFieldActive: boolean = false;

  ShowEvent(e: any){
    console.log(e);
 }

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

  onFocusPassword(): void {
    this.IsPasswordFieldActive = true;
    console.log(true)
  }

  onBlurPassword(): void {
    this.IsPasswordFieldActive = false;
    console.log(false)

  }

  protected readonly console = console;
}
