import {Component, Input} from '@angular/core';
import {RouterHelperService} from "../../../services/router-helper.service";

@Component({
  selector: 'app-job-request-chip',
  templateUrl: './job-request-chip.component.html',
  styleUrls: ['./job-request-chip.component.scss']
})
export class JobRequestChipComponent {
  @Input() status: string | undefined;
  @Input() finished: boolean = false;

  constructor(
  ) { }


  ngOnInit(): void {

  }
}
