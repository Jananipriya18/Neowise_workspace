import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    const url: string = state.url;
    const user = localStorage.getItem('userRole');

    if (user) {
      console.log(user);

      if (this.isAdminRoute(url) && user != 'Seller') {
        console.log("farmer entering in seller route");
        this.router.navigate(['/error']);
        return false;
      }

      if (this.isCustomerRoute(url) && user != 'Farmer') {
        console.log("seller entering in farmer route");
        this.router.navigate(['/error']);
        return false;
      }

      if (this.isCommonRoute(url)) {
        return true;
      }

      // Navigate to not found page if user tries to access a page where they do not have access
      return true;
    }

    // Navigate to login page if user is not authenticated
    this.router.navigate(['/login']);
    return false;
  }

  private isAdminRoute(url: string): boolean {
    const adminRoutes = [ 'seller/add/newchemical','seller/view/requests', 'seller/view/chemical', 'seller/edit/chemical/:id', 'admin/view/feedback', 'admin/view/requestedloan' ,'supplier/edit/medicine'];
    return adminRoutes.some(route => url.includes(route));
  }

  private isCustomerRoute(url: string): boolean {
    const customerRoutes = [ 'farmer/add/newcrop', 'farmer/view/crop', 'farmer/edit/crop/:id', 'user/add/feedback', 'user/view/appliedloan', 'farmer/view/chemical','farmer/view/myrequests'];
    return customerRoutes.some(route => url.includes(route));
  }

  private isCommonRoute(url: string): boolean {
    const commonRoutes = ['', 'login','signup'];
    return commonRoutes.some(route => url.includes(route));
  }
}

//final
