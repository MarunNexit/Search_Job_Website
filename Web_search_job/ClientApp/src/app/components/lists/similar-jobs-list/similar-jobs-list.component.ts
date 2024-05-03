import { Component } from '@angular/core';

interface DataJob {
  id: number;
  title: string;
  salary:string;
  company:string;
  description: string;
  tags: string[];
  company_picture:string;
  banner_picture:string;
  hot_new_marks:boolean[];

}

@Component({
  selector: 'app-similar-jobs-list',
  templateUrl: './similar-jobs-list.component.html',
  styleUrls: ['./similar-jobs-list.component.scss']
})
export class SimilarJobsListComponent {

  dataJobs: DataJob[] = [];
  constructor() {

    // Створення трьох екземплярів об'єктів DataJob
    let dataJob1: DataJob = {
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

    let dataJob2: DataJob = {
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

    let dataJob3: DataJob = {
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


    this.dataJobs[0] = dataJob1;
    this.dataJobs[1] = dataJob2;
    this.dataJobs[2] = dataJob3;
  }
}
