import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {MyErrorStateMatcher} from "../../../auth/error-state-matcher";
import {AuthService} from "../../../models/backend/dtos/auth/auth-service";
import {ActivatedRoute, Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {passwordValidator} from "../../../auth/password.validator";
import {ScreenSizeService} from "../../../services/screen-size.sevice";
import {RouterHelperService} from "../../../services/router-helper.service";

@Component({
  selector: 'app-registration-auth-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.scss'],
})
export class RegistrationPageComponent implements OnInit {
  isScreenSmall: boolean = false;
  IsPasswordFieldActive: boolean = false;

  isSearcher: boolean = true;

  public registerForm!: FormGroup;
  public matcher = new MyErrorStateMatcher();

  isSelectedInput: boolean[] = [false, false, false, false, false, false, false]
  ShowEvent(e: any){
    //console.log(e);
 }

  IsSearcher(isSearcher: boolean){
    this.isSearcher = isSearcher;
  }

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackbar: MatSnackBar,
    private screenSizeService: ScreenSizeService,
    private route: ActivatedRoute,
    private routerHelperService: RouterHelperService,

  ) {
    this.screenSizeService.setIsSmallScreen('(max-width: 1200px)');

    this.screenSizeService.isScreenSmall$.subscribe(isSmall => {
      this.isScreenSmall = isSmall;
    })
  }



  ngOnInit(): void {


    this.route.queryParamMap.subscribe(params => {
      const role = params.get('role');
      //console.log(role);
      if(role){
        if (role === "employer") {
          this.isSearcher = false;
        }
        else{
          this.isSearcher = true;
        }
      }


    });


    /*const registerButton = document.getElementById("register-button");
    const loginButton = document.getElementById("auth-button");
    const container = document.getElementById("container_new");




    if (registerButton && container){
      registerButton.addEventListener("click", () => {
        container.classList.add("right-panel-active");
      });
    }

    if (loginButton && container){
      loginButton.addEventListener("click", () => {
        container.classList.remove("right-panel-active");
      });
    }*/

    this.registerForm = new FormGroup({
      username: new FormControl('', [Validators.required]),
      lastname: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, passwordValidator()]),
    });
  }


  public onSubmit() {
    if (this.isSearcher){
      this.authService
        .register(
          /*
                  this.registerForm.get('username')?.value,
          */
          this.registerForm.get('email')?.value,
          this.registerForm.get('password')?.value
        )
        .subscribe(() => {
          this.snackbar.open('User registered.', 'close');
          this.goToURL('auth-login', true);
        });
    }
    else{
      this.authService
        .registerEmployer(
          /*
                  this.registerForm.get('username')?.value,
          */
          this.registerForm.get('email')?.value,
          this.registerForm.get('password')?.value
        )
        .subscribe(() => {
          this.snackbar.open('User registered.', 'close');
          this.goToURL('auth-login', true);
        });
    }

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

  protected readonly console = console;
  protected readonly String = String;
}
