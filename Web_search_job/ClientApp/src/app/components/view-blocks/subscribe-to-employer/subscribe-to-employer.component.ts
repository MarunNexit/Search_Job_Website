import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-subscribe-to-employer',
  templateUrl: './subscribe-to-employer.component.html',
  styleUrls: ['./subscribe-to-employer.component.scss']
})
export class SubscribeToEmployerComponent {
  @Input() dataEmployer: any;
  @Input() isShortView: boolean = false;
  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    const selectedValue = target.value;
    console.log('Selected value:', selectedValue);
  }
}
