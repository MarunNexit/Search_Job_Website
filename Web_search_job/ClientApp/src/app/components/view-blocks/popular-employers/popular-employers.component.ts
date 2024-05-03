import {Component, Input} from '@angular/core';


@Component({
  selector: 'app-popular-employers',
  templateUrl: './popular-employers.component.html',
  styleUrls: ['./popular-employers.component.scss']
})
export class PopularEmployersComponent {
  @Input() isPage: boolean = false;

  employers = [
    { name: 'Company 1', field: 'Field 1' },
    { name: 'Company 2', field: 'Field 2' },
    { name: 'Company 3', field: 'Field 3' },
    { name: 'Company 3', field: 'Field 3' },
    { name: 'Company 3', field: 'Field 3' },
    { name: 'Company 3', field: 'Field 3' },
    { name: 'Company 3', field: 'Field 3' },
    { name: 'Company 3', field: 'Field 3' }
  ];
}
