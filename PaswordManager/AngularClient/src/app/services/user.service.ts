import { Injectable } from '@angular/core';
import { HttpClient,HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user/user';
import { UserInput } from '../models/user/userInput';
import { Guid } from 'guid-ts';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = 'https://localhost:7063/api/Users';

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<User[]>{
    return this.http.get<User[]>(this.baseUrl,{withCredentials:true});
  }

  getDetailsOfUser(id: Guid):Observable<UserInput>{
    return this.http.get<UserInput>(this.baseUrl + '/' + id);
  }

  getCurrentUser():Observable<UserInput>{
    return this.http.get<UserInput>(this.baseUrl + '/GetCurrentUser');
  }

  deleteUser(id: Guid): Observable<User>{
    return this.http.delete<User>(this.baseUrl + '/' + id);
  }

  registrationUser(registrationUser: UserInput): Observable<UserInput>{
    return this.http.post<UserInput>(this.baseUrl + `/Registration`,registrationUser);
  }

  editUser(user: UserInput): Observable<UserInput>{
    return this.http.put<UserInput>(this.baseUrl + '/Edit', user);
  }

  login(name:string, password:string){
    return this.http.post<any>(this.baseUrl + `/Login`, {name, password});
  }

  logout(){
    return this.http.post(this.baseUrl + '/Logout',{});
  }
}
