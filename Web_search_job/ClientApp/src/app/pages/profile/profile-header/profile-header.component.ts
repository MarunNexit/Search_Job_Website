import {Component, Input} from '@angular/core';
import {handleImageError} from "../../../functions/handleImageError";
import {UserData} from "../../../models/backend/dtos/auth/dtos/user-data";
import {AuthenticationClient} from "../../../models/backend/dtos/auth/auth-client";
import {Router} from "@angular/router";
import {JobShortDTO} from "../../../models/backend/dtos/jobs/job-short.dto";
import {RouterHelperService} from "../../../services/router-helper.service";
import {ProfileDataService} from "../../../services/backend/profile-data.service";
import {ResumeDTO} from "../../../models/backend/dtos/profiles/resume.dto";
import {PopupPurpleComponent} from "../../../components/popup/popup-purple/popup-purple.component";
import {MatDialog} from "@angular/material/dialog";


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

  @Input() userData: UserData;
  @Input() isMyProfile: boolean = false;
  @Input() Role: string;

  profileData: ResumeDTO | null;
  user: UserData;
  public links_: Link[] = []; // Ініціалізація масиву

  constructor(
    private router: Router,
    private routerHelperService: RouterHelperService,
    private profileDataService: ProfileDataService,
    public dialog: MatDialog,
  ) {

  }

  ngOnInit(){
    this.links_.push(
      { type: 'dribbble', link: '/' },
      { type: 'behance', link: '/' },
      { type: 'facebook', link: '/' },
      { type: 'linkedin', link: '/' }
    );

    this.profileDataService.getProfileData().subscribe(profileData => {
      this.profileData = profileData;
      console.log(this.profileData)
    });
  }

  GetActiveResumeData(){

  }

  onErrorLoad(){
    this.router.navigate(['/']);
    this.routerHelperService.goToUrl('/', true)
  }

  navigate(url: string) {
    window.open(url, '_blank');
  }

  openDialogAnalytics(): void {
    if(this.userData && this.isMyProfile && this.Role == "Searcher"){
      const dialogRef = this.dialog.open(PopupPurpleComponent, {
        width: '1000px',
        data: {type: "searcherAnalytics", title: "Аналітика діяльності", user: this.userData}
      });

      dialogRef.afterClosed().subscribe(result => {
        console.log(result);
      });
    }
  }

  protected readonly handleImageError = handleImageError;
}
