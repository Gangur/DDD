import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { API_BASE_URL, Client } from './api/http-client';
import { HomeModule } from './home/home.module';

function getBaseUrl() {
  return 'https://localhost:44370/';
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    HomeModule
  ],
  providers: [
    Client,
    { provide: API_BASE_URL, useValue: 'https://localhost:44370' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
