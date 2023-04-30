import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule, HttpClientXsrfModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Users/login/login.component';
import { EditComponent } from './Users/edit/edit.component';
import { FormsModule }   from '@angular/forms';
import {AddWithCredentialsInterceptorService} from './http-interceptor.service';
import { RegistrationComponent } from './Users/registration/registration.component'

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    EditComponent,
    RegistrationComponent
  ],
  imports: [
    FormsModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass:AddWithCredentialsInterceptorService, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
