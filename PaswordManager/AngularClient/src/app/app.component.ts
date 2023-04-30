import { Component } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import {UserInput} from "./models/user/userInput";
import {Guid} from "guid-ts";
import {isEmpty} from "rxjs-compat/operator/isEmpty";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private userService:UserService) { }
  title = 'AngularClient';
  user:UserInput = {
    id:Guid.empty().toString(),
    name:'',
    eMail:'',
    password:''
  }
  getCurrentUser(){
    this.userService.getCurrentUser().subscribe(Response=>{
      this.user = Response
    })
  }
  Logout(){
    this.userService.logout().subscribe(()=>this.userService.getCurrentUser().subscribe(Response=>{

      this.user.name=''
    }));
  }
  ngOnInit(): void {
    this.getCurrentUser();
  }
}
