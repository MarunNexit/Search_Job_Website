import { Injectable } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class IdFromUrlService {
  private idSubject = new BehaviorSubject<string | null>(null);

  constructor(private route: ActivatedRoute) {
    this.route.params.pipe(
      map(params => params['id'])
    ).subscribe(id => {
      this.idSubject.next(id);
    });
  }

  getId() {
    return this.idSubject.asObservable();
  }
}
