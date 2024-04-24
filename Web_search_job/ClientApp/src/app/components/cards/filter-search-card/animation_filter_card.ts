import { trigger, state, style, transition, animate } from '@angular/animations';

export const expandCollapse = trigger('expandCollapse', [
  state('collapsed', style({
    height: '0',
    opacity: '0',
    overflow: 'hidden'
  })),
  state('expanded', style({
    height: '*',
    opacity: '1',
    overflow: 'visible'
  })),
  transition('collapsed <=> expanded', [
    animate('0.3s ease-in-out')
  ])
]);
