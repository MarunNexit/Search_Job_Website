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
      title: "Гарне робоче місце",
      people_name: "Marian",
      people_description: "Супервайзор (діючий) - 3 Березня 2021",
      text: "Супервайзор Супервайзор  Супервайзор Супервайзор (діючий співробітник) - 3 Березня 2021 Супервайзор (діючий співробітник) - 3 Березня 2021Супервайзор (діючий співробітник) - 3 Березня 2021Супервайзор (діючий співробітник) ",
    };

    let dataJob2 = {
      id: 2,
      title: "Менеджер з продажів",
      salary: "70000 USD",
      company: "XYZ Corp.",
      people_description: "Опис посади менеджера з продажів",
      tags: ["Продажі", "Маркетинг", "Бізнес"],
      company_picture: "../../../../assets/img/loading-picture.png",
      banner_picture: "",
      text: "Супервайзор (діючий співробітник) - 3 Березня 2021 Супервайзор (діючий співробітник) - 3 Березня 2021Супервайзор (діючий співробітник) - 3 Березня 2021Супервайзор (діючий співробітник) - 3 Березня 2021",
    };

    let dataJob3 = {
      id: 3,
      title: "Дизайнер UX/UI",
      salary: "80000 USD",
      company: "DEF Design Studio",
      people_description: "Опис посади дизайнера UX/UI",
      tags: ["Дизайн", "UX", "UI"],
      company_picture: "url/to/company_picture_3.jpg",
      banner_picture: "",
      text: "Супервайзор (діючий співробітник) - 3 Березня 2021 Супервайзор (діючий співробітник) - 3 Березня 2021Супервайзор (діючий співробітник) - 3 Березня 2021Супервайзор (діючий співробітник) - 3 Березня 2021",
    };


    this.dataEmployerComments[0] = dataJob1;
    this.dataEmployerComments[1] = dataJob2;
    this.dataEmployerComments[2] = dataJob3;
  }
}
