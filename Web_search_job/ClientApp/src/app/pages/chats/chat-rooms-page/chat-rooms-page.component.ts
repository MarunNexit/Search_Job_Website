import { Component } from '@angular/core';
import {ChatsService} from "../../../services/chats.service";
import {ScreenSizeService} from "../../../services/screen-size.sevice";
import {MatCheckboxChange} from "@angular/material/checkbox";

@Component({
  selector: 'app-chat-rooms-page',
  templateUrl: './chat-rooms-page.component.html',
  styleUrls: ['./chat-rooms-page.component.scss']
})
export class ChatRoomsPageComponent {
  selectedOption: string = 'new';
  sortedValue: string[] = [];
  isScreenSmall: boolean = false;
  onlyVacancy: boolean = false;
  onlySaved: boolean = false;

  constructor(private chatsService: ChatsService, private screenSizeService: ScreenSizeService) {
  }

  ngOnInit(){
    this.screenSizeService.setIsSmallScreen('(max-width: 1200px)');

    this.screenSizeService.isScreenSmall$.subscribe(isSmall => {
      this.isScreenSmall = isSmall;
    });
  }
  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    const selectedValue = target.value;
    this.sortedValue[0] = target.value;
    this.selectedOption = target.value;

    //this.searchParamService.setParamSorting(this.sortedValue);
  }
  onChangeCheckbox($event: MatCheckboxChange, targetName: string){
    const checkboxValue = $event.checked; // Отримання значення чекбокса з $event
    if(targetName == 'vacancy'){
      this.onlyVacancy = checkboxValue;
    }
    else if(targetName == 'vacancy')
    {
      this.onlySaved = checkboxValue;
    }
  }
}
