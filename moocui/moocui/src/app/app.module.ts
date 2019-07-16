import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core'
import {CommonModule }from '@angular/common'; 
import {HTTP_INTERCEPTORS, HttpClientModule }from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './views/home/home.component';
import { ApiService } from './services/api.service';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule
  ],
  providers: [
    ApiService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
