import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { throwError as observableThrowError } from "rxjs";
import { catchError } from "rxjs/operators";
import { environment } from 'src/environments/environment';


@Injectable()
export class ApiService {

  constructor(private http: HttpClient) { }

  // private updateUrl(url: string): string {
  //   var link = environment.apiPath + url;
  //   return link;
  // }


  // private getHeaders(hasBody: boolean) {
  //   // const headers: any = {};

  //   //token based not in use
  //   /*
  //   if (token) {
  //       headers["Authorization"] = "Bearer " + token;
  //   }
  //   */
  //   var headers = new HttpHeaders;
  //   headers.set('Content-Type', 'application/json')
  //   return headers;
  // }

// public post<T> (id: number): Observable<T> => {
//     let $data = new FormData();
//     $data.append('id', String(id));
//     return this.http.post<T>( 'Subject/DeleteSubjectById', $data, {});
// }
//   public  post<T>(url: string, body: any): Promise<T> {
//     //this.spinner.show();
//     return this.http.post<T>(this.updateUrl(url), data, {});
//     // const response = await this.http
//     //   .post<T>(this.updateUrl(url), body, { headers: this.getHeaders(true), withCredentials: true, observe: "response" })
//     //   .pipe(
//     //     catchError((res) => {
//     //       // this.spinner.hide();
//     //       return observableThrowError(res);
//     //     })
//     //   )
//     //   .toPromise();

//     // //this.spinner.hide();
//     // return response.body;
//   }

//   public async get<T>(url: string): Promise<T> {
//     // this.spinner.show();
//     debugger;
//     const response = await this.http
//       .get<T>(this.updateUrl(url), { headers: this.getHeaders(true), withCredentials: true, observe: "response" })
//       .pipe(
//         catchError((res) => {
//           // this.spinner.hide();
//           return observableThrowError(res);
//         })
//       )
//       .toPromise();

//     //this.spinner.hide();
//     return response.body;
//   }



}