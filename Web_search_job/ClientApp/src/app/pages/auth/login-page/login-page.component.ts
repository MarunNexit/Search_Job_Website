import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../../models/backend/dtos/auth/auth-service";
import {Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ScreenSizeService} from "../../../services/screen-size.sevice";
import {passwordValidator} from "../../../auth/password.validator";
import {MyErrorStateMatcher} from "../../../auth/error-state-matcher";
import {RouterHelperService} from "../../../services/router-helper.service";

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent {
  isScreenSmall: boolean = false;
  public loginForm!: FormGroup;
  public matcher = new MyErrorStateMatcher();
  IsPasswordFieldActive: boolean = false;

  isSelectedInput: boolean[] = [false, false]


  constructor(
    private authService: AuthService,
    private router: Router,
    private snackbar: MatSnackBar,
    private screenSizeService: ScreenSizeService,
    private routerHelperService:RouterHelperService
  ) {
    this.screenSizeService.setIsSmallScreen('(max-width: 1200px)');

    this.screenSizeService.isScreenSmall$.subscribe(isSmall => {
      this.isScreenSmall = isSmall;
    })
  }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, passwordValidator()]),
    });
  }

  public onSubmit() {
    this.authService.login(
      this.loginForm.get('email')?.value,
      this.loginForm.get('password')?.value
    );

  }

  goToURLWithParam(url: string, b: boolean, s: string) {
    this.routerHelperService.goToUrlWithParam(url, b, s);
  }

  goToURL(url: string, b: boolean) {
    this.routerHelperService.goToUrl(url, b);
  }

  onFocusPassword(): void {
    this.IsPasswordFieldActive = true;
  }

  onBlurPassword(): void {
    this.IsPasswordFieldActive = false;
  }
  onInputSelected(num: number): void {
    this.isSelectedInput[num] = true;
  }

}
