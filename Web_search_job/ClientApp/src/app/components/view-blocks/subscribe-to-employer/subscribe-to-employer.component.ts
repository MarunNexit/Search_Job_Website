import {ChangeDetectorRef, Component, Input} from '@angular/core';
import {EmployerDTO} from "../../../models/backend/dtos/employers/employer-full.dto";
import {catchError} from "rxjs";
import {OtherService} from "../../../services/backend/other.service";
import {Location} from "../../../models/backend/dtos/other/location.dto";
import {Industry} from "../../../models/backend/dtos/other/industry.dto";
import {LocationDataDTO} from "../../../models/backend/dtos/other/location-data.dto";
import {UserService} from "../../../services/backend/user.service";
import {UserData} from "../../../services/backend/auth/dtos/user-data";

@Component({
  selector: 'app-subscribe-to-employer',
  templateUrl: './subscribe-to-employer.component.html',
  styleUrls: ['./subscribe-to-employer.component.scss']
})
export class SubscribeToEmployerComponent {
  @Input() dataEmployer: EmployerDTO;
  @Input() isShortView: boolean = false;
  @Input() isRightPanel: boolean = false;
  @Input() isSmallScreen: boolean = false;


  isLoadedLocations = false;
  isLoadedIndustries = false;
  hasError = false;

  locations: LocationDataDTO[];
  industries: Industry[];

  userData: UserData | null = null;



  constructor(private otherService: OtherService, private cdr: ChangeDetectorRef, private userService: UserService) {

  }

  ngOnInit(){
    this.userService.getUserData().subscribe(userData => {
      this.userData = userData;
      console.log(userData)
      if (this.userData && this.dataEmployer) {
        this.getLocations(this.userData.id, '', this.dataEmployer.location);
        this.getIndustries('ІТ', this.dataEmployer.industry.industry_name);
      }
      else {
        this.getLocations(null, 'Івано-Франківськ', this.dataEmployer.location);
        this.getIndustries('ІТ', this.dataEmployer.industry.industry_name);
      }
    });
  }

  getLocations(userId: string | null, anonymLocation: string = "", employerLocation: LocationDataDTO): void {
    this.otherService
      .getLocations(userId, anonymLocation, employerLocation)
      .pipe(
        catchError(() => {
          this.isLoadedLocations = false;
          this.hasError = true;
          return []
        })
      )
      .subscribe((result: LocationDataDTO[]) => {
        this.locations = result;
        this.isLoadedLocations = true;
        this.hasError = false
        console.log(this.locations);

        this.cdr.detectChanges();
      });
  }


  getIndustries(anonymIndustry: string, employerIndustry: string): void {
    this.otherService
      .getIndustries(anonymIndustry, employerIndustry)
      .pipe(
        catchError(() => {
          this.isLoadedIndustries = false;
          this.hasError = true;
          return []
        })
      )
      .subscribe((result: Industry[]) => {
        this.industries = result;
        this.isLoadedIndustries = true;
        this.hasError = false
        console.log(this.industries);

        this.cdr.detectChanges();
      });
  }


  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    const selectedValue = target.value;
  }
}
