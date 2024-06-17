import { Component } from '@angular/core';
import {AuthService} from "../../models/backend/dtos/auth/auth-service";
import {RouterHelperService} from "../../services/router-helper.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  Role: string = 'ANONYM';
  isLogin: boolean = false;

  constructor(
    private routerHelperService: RouterHelperService,
    private authService: AuthService,
  ) {
  }

  ngOnInit(){
    this.authService.isLoggedIn().subscribe(result =>{
      this.isLogin = result;
      if(result){
        this.authService.getRole().subscribe(result =>{
          this.Role = result;
        })
      }
    })

  }
}
