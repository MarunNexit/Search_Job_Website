import { Component } from '@angular/core';
import {handleImageError} from "../../../functions/handleImageError";
import {UserData} from "../../../services/backend/auth/dtos/user-data";
import {AuthenticationClient} from "../../../services/backend/auth/auth-client";
import {Router} from "@angular/router";


class Profile {
  public id: number
  public company_picture: string
  public email: string
  public age: number
  public bio: string
  protected readonly handleImageError = handleImageError;
}

interface Link {
  type: string;
  link: string;
}

@Component({
  selector: 'app-profile-header',
  templateUrl: './profile-header.component.html',
  styleUrls: ['./profile-header.component.scss']
})


export class ProfileHeaderComponent {

  profileData: any = [];
  user: UserData;
  public links_: Link[] = []; // Ініціалізація масиву

  constructor(
    private router: Router,
  ) {

  }

  ngOnInit(){
    this.links_.push(
      { type: 'dribbble', link: '/' },
      { type: 'behance', link: '/' },
      { type: 'facebook', link: '/' },
      { type: 'linkedin', link: '/' }
    );
  }

  navigate(link: string) {
    this.router.navigate([link]);
  }

  protected readonly handleImageError = handleImageError;
}
