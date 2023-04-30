import { Component, OnInit } from '@angular/core';
import { Guid } from 'guid-ts';
import { UserService } from 'src/app/services/user.service';
import { UserInput } from '../../models/user/userInput';
import {AppComponent} from 'src/app/app.component'
import {Router} from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registrationUser:UserInput={
    id:Guid.empty().toString(),
    name:'',
    password:'',
    eMail:''
  }

  constructor(private userService:UserService, private appComponentUser: AppComponent, private route: Router) { }

  ngOnInit(): void {
  }

  RegistrationUser(){
    //Todo Нужно переделывать под условие того что, если придет в ответе RegistrationUser()-null то не делат лишний запрос к БД
    this.userService.registrationUser(this.registrationUser).subscribe(
        ()=>this.userService.getCurrentUser().subscribe(Response=>
        this.appComponentUser.user = Response))
  }
}
