import { Component, OnInit } from '@angular/core';
import { Guid } from 'guid-ts';
import { UserService } from 'src/app/services/user.service';
import { UserInput } from 'src/app/models/user/userInput';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  userInput:UserInput = {
    id:Guid.empty().toString(),
    name:'', 
    eMail:'', 
    password:''
  } 

  constructor(private userService:UserService) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  getCurrentUser(){
    this.userService.getCurrentUser().subscribe(Response=>{
      this.userInput = Response
    })
  }

  userEdit(){
    this.userService.editUser(this.userInput).subscribe(Response=>{
      this.userInput = Response
      this.getCurrentUser();
    })
  }
}
