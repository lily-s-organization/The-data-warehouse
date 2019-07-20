import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { ApiService } from '../../services/api.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private http: HttpClient,private api: ApiService) { }

  private singleTeacher: any;
  private teacherList: any;

  ngOnInit() {
    this.GetTeacherList();
    this.GetTeacherById(5);
  }

  async GetTeacherList() {
    var url = environment.apiPath+"api/teacher";

    this.http.get(url).subscribe(res => 
      {
         console.log(res);
         this.teacherList = res;
      })
  }

  async GetTeacherById(id) {
    var url = environment.apiPath+"api/teacher/"+id;

    this.http.get(url).subscribe(res => 
      {
         console.log(res);
         this.singleTeacher = res;
      })
  }

  async PostList() {
    var url = environment.apiPath+"api/values";

    this.http.post(url,null).subscribe(res => 
      {
         console.log(res);
      })

  }
}
