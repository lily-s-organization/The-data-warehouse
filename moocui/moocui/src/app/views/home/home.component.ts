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

  ngOnInit() {
    this.PostList();
  }

  async GetList() {
    var url = environment.apiPath+"api/values";

    this.http.get(url).subscribe(res => 
      {
         console.log(res);
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
