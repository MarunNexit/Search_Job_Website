import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-highest-paying-jobs',
  templateUrl: './highest-paying-jobs.component.html',
  styleUrls: ['./highest-paying-jobs.component.scss']
})
export class HighestPayingJobsComponent {

  @Input() filter: string = '';

  employers = [
    { name: 'Company 1', field: 'Field 1' },
    { name: 'Company 2', field: 'Field 2' },
    { name: 'Company 3', field: 'Field 3' },
  ];

  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    const selectedValue = target.value;
    console.log('Selected value:', selectedValue);
  }

}
