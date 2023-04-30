import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditComponent } from './Users/edit/edit.component';
import { LoginComponent } from './Users/login/login.component';
import { RegistrationComponent } from './Users/registration/registration.component';
import {AuthGuard} from "./auth.guard";

const routes: Routes = [
  {path:'users/edit', component: EditComponent, canActivate: [AuthGuard]},
  {path:'users/login', component: LoginComponent},
  {path:'users/registration', component: RegistrationComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
