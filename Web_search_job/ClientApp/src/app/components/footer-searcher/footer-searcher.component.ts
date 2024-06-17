import { Component } from '@angular/core';
import {RouterHelperService} from "../../services/router-helper.service";
import {AuthService} from "../../models/backend/dtos/auth/auth-service";

@Component({
  selector: 'app-footer-searcher',
  templateUrl: './footer-searcher.component.html',
  styleUrls: ['./footer-searcher.component.scss']
})
export class FooterSearcherComponent {
  isLogin: boolean = false;
  Role: string = 'ANONYM'
  constructor(
    private routerHelperService: RouterHelperService,
    private authService: AuthService,
  ) {
  }

  goToURL(url: string){
    this.routerHelperService.goToUrl(url, true);
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
