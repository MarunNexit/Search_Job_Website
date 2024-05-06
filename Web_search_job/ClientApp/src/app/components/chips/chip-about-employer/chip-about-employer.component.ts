import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-chip-about-employer',
  templateUrl: './chip-about-employer.component.html',
  styleUrls: ['./chip-about-employer.component.scss']
})
export class ChipAboutEmployerComponent {
  @Input() dataChip: string;
}
