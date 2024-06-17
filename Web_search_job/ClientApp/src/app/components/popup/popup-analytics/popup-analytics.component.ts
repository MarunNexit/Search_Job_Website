import {Component, Input} from '@angular/core';
import {AuthenticationClient} from "../../../models/backend/dtos/auth/auth-client";
import {flatMap} from "rxjs";

@Component({
  selector: 'app-popup-analytics',
  templateUrl: './popup-analytics.component.html',
  styleUrls: ['./popup-analytics.component.scss']
})
export class PopupAnalyticsComponent {
  @Input() Data: any;
  @Input() Type: any;
  isloading: boolean = true;
  resultData: any;

  hourPeriod: string;
  dayOfWeek: string;

  constructor(
    private authClient: AuthenticationClient,
  ) {
  }

  ngOnInit(){
    console.log(this.Data)
    if(this.Data.user.userId){
      console.log("isTRUE")
    this.authClient.getStatisticsSearcher(this.Data.user.userId)
      .subscribe(
        (data) => {
          this.resultData = data;
          console.log(this.resultData)
          this.calculateHourPeriod(data);
          this.calculateDayOfWeek(data);
          this.isloading = false;
        },
        (error) => {
          console.log('Failed to load statistics')
          this.isloading = false;
        }
      );
    }
  }

  calculateHourPeriod(data: any){
    if(data.mostActiveHourPeriod){
      var hour = data.mostActiveHourPeriod * 2;
      var hourlast = hour.toString();
      if(hour == 24){ hourlast = '00' }
      if(hour){
        this.hourPeriod = (hour-2) + ":00-" + hour + ':00'
      }
    }
    else {
      this.hourPeriod = 'Немає';
    }
  }


  calculateDayOfWeek(data: any){
    const dayNames = [
      'Неділя',    // 0
      'Понеділок', // 1
      'Вівторок',  // 2
      'Середа',    // 3
      'Четвер',    // 4
      'П’ятниця',  // 5
      'Субота'     // 6
    ];

    if (data.mostPopularDayOfWeek !== undefined) {
      this.dayOfWeek = dayNames[data.mostPopularDayOfWeek - 1];
    } else {
      this.dayOfWeek = 'Недостатньо даних';
    }
  }
}
