import { Component } from '@angular/core';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent {

  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    const selectedValue = target.value;
  }
}
