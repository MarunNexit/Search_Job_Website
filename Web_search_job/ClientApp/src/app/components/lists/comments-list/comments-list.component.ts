import { Component } from '@angular/core';

@Component({
  selector: 'app-comments-list',
  templateUrl: './comments-list.component.html',
  styleUrls: ['./comments-list.component.scss']
})
export class CommentsListComponent {

  dataEmployerComments:any[] = [];
  constructor() {

    // Створення трьох екземплярів об'єктів DataJob
    let dataJob1 = {
      id: 1,
      title: "Розробник програмного забезпечення",
      salary: "90000 USD",
      company: "ABC Inc.",
      description: "Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення Опис посади розробника програмного забезпечення",
      tags: ["IT", "Програмування", "Розробка"],
      company_picture: "../../../../assets/img/icons/cards/check_mark.png",
      banner_picture: "url/to/banner_picture_1.jpg",
      hot_new_marks: [true, true, false] // Приклад оцінок "гарячої новини"
    };

    let dataJob2 = {
      id: 2,
      title: "Менеджер з продажів",
      salary: "70000 USD",
      company: "XYZ Corp.",
      description: "Опис посади менеджера з продажів...",
      tags: ["Продажі", "Маркетинг", "Бізнес"],
      company_picture: "../../../../assets/img/loading-picture.png",
      banner_picture: "",
      hot_new_marks: [false, false, false] // Приклад оцінок "гарячої новини"
    };

    let dataJob3 = {
      id: 3,
      title: "Дизайнер UX/UI",
      salary: "80000 USD",
      company: "DEF Design Studio",
      description: "Опис посади дизайнера UX/UI...",
      tags: ["Дизайн", "UX", "UI"],
      company_picture: "url/to/company_picture_3.jpg",
      banner_picture: "",
      hot_new_marks: [false, false, true] // Приклад оцінок "гарячої новини"
    };


    this.dataEmployerComments[0] = dataJob1;
    this.dataEmployerComments[1] = dataJob2;
    this.dataEmployerComments[2] = dataJob3;
  }
}
