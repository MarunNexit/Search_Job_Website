import { Component } from '@angular/core';


interface Filter {
  name: string;
  checked: boolean;
}

interface FilterComponent {
  name: string;
  filters: Filter[];
}


@Component({
  selector: 'app-filter-search-list',
  templateUrl: './filter-search-list.component.html',
  styleUrls: ['./filter-search-list.component.scss']
})


export class FilterSearchListComponent {
  filters_data: FilterComponent[] = [
    {
      name: 'Професійна сфера',
      filters: [
        { name: 'IT', checked: false },
        { name: 'Адміністративний персонал - Водії - Кур\'єри', checked: false },
        { name: 'Дизайн - Графіка - Фото', checked: false },
        { name: 'Культура - Шоу-бізнес - Розваги', checked: false },
        { name: 'Маркетинг - Реклама - PR', checked: false },
      ]
    },
    {
      name: 'Характер роботи',
      filters: [
        { name: 'Віддалено', checked: false },
        { name: 'В офісі', checked: false },
        { name: 'Гібридно', checked: false },
      ]
    },
    {
      name: 'Рівень зарплати',
      filters: [
        { name: 'Від 10 000 грн', checked: false },
        { name: 'Від 15 000 грн', checked: false },
        { name: 'Від 20 000 грн', checked: false },
        { name: 'Від 30 000 грн', checked: false },
        { name: 'Від 50 000 грн', checked: false },
      ]
    },
    {
      name: 'Розташування',
      filters: [
        { name: 'Київ', checked: false },
        { name: 'Дніпро', checked: false },
        { name: 'Харків', checked: false },
        { name: 'Запоріжжя', checked: false },
        { name: 'Івано-Франківськ', checked: false },
      ]
    },
    {
      name: 'Рівень необхідного досвіду',
      filters: [
        { name: 'Початковий рівень', checked: false },
        { name: 'Середній', checked: false },
        { name: 'Експерт', checked: false },
      ]
    },

    {
      name: 'Тип зайнятості',
      filters: [
        { name: 'Повний робочий день', checked: false },
        { name: 'Неповний робочий день', checked: false },
        { name: 'Позмінна робота', checked: false },
        { name: 'Тимчасова робота', checked: false },
        { name: 'Навчання', checked: false },
        { name: 'Стажування', checked: false },
      ]
    },

    {
      name: 'Пошуковий запит',
      filters: [
        { name: 'Лише вакансії в ЗСУ', checked: false },
        { name: 'Приховати Гарячі вакансії', checked: false },
      ]
    },

  ];
  addingSearchField:boolean[] = [
    true,
    false,
    false,
    true,
    false,
    false,
    false,
  ]
}
