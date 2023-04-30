import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import {EditComponent} from 'src/app/Users/edit/edit.component'

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean{
    return true;
  }
}
