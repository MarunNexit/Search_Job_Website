import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {MyErrorStateMatcher} from "../../../auth/error-state-matcher";
import {AuthService} from "../../../services/backend/auth/auth-service";
import {Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {passwordValidator} from "../../../auth/password.validator";

@Component({
  selector: 'app-registration-auth-page',
  templateUrl: './registration-login-page.component.html',
  styleUrls: ['./registration-login-page.component.scss'],
})
export class RegistrationLoginPageComponent implements OnInit {

  IsPasswordFieldActive: boolean = false;
  public registerForm!: FormGroup;
  public matcher = new MyErrorStateMatcher();

  ShowEvent(e: any){
    console.log(e);
 }

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackbar: MatSnackBar
  ) {}

  ngOnInit(): void {

    this.registerForm = new FormGroup({
      username: new FormControl(''),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, passwordValidator()]),
    });

    const registerButton = document.getElementById("register");
    const loginButton = document.getElementById("auth");
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


  public onSubmit() {
    this.authService
      .register(
        this.registerForm.get('username')?.value,
        this.registerForm.get('email')?.value,
        this.registerForm.get('password')?.value
      )
      .subscribe(() => {
        this.snackbar.open('User registered.', 'close');
        this.router.navigate(['login']);
      });
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
