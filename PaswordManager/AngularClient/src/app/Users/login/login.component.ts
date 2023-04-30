import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import {AppComponent} from 'src/app/app.component'


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  loginForm: any = {
    name: '',
    password: '',
  }

  constructor(private userService:UserService, private appComponentUer:AppComponent) { }

  ngOnInit(): void {

  }

  Login(){
    //Todo Нужно переделывать под условие того что, если придет в ответе Login()-null то не делат лишний запрос к БД
    this.userService.login(this.loginForm.name, this.loginForm.password).subscribe(
        ()=>this.userService.getCurrentUser().subscribe(
            Response=>{
                    this.appComponentUer.user = Response
    }));
    
  }
}
