import { Injectable, OnDestroy } from '@angular/core';
import { Observable, BehaviorSubject, of, Subscription } from 'rxjs';
import { map, catchError, switchMap, finalize } from 'rxjs/operators';
import { UserModel } from '../models/user.model';
import { AuthModel } from '../models/auth.model';
import { AuthHTTPService } from './auth-http';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Result } from '../models/result';
import { userToFormData } from 'src/app/_helpers/mapper';

export type UserType = AuthModel | undefined;

@Injectable({
  providedIn: 'root',
})
export class AuthService implements OnDestroy {
  // private fields
  private unsubscribe: Subscription[] = []; // Read more: => https://brianflove.com/2016/12/11/anguar-2-unsubscribe-observables/
  private authLocalStorageToken = `${environment.appVersion}-${environment.USERDATA_KEY}`;
  private baseUrl = environment.baseUrl;


  // public fields
  currentUser$: Observable<UserType>;
  isLoading$: Observable<boolean>;
  currentUserSubject: BehaviorSubject<UserType>;
  isLoadingSubject: BehaviorSubject<boolean>;

  get currentUserValue(): UserType {
    return this.currentUserSubject.value;
  }

  set currentUserValue(user: UserType) {
    this.currentUserSubject.next(user);
  }

  constructor(
    private authHttpService: AuthHTTPService,
    private router: Router,
    private http: HttpClient
  ) {
    this.isLoadingSubject = new BehaviorSubject<boolean>(false);
    this.currentUserSubject = new BehaviorSubject<UserType>(undefined);
    this.currentUser$ = this.currentUserSubject.asObservable();
    this.isLoading$ = this.isLoadingSubject.asObservable();
    const subscr = this.getUserByToken().subscribe();
    this.unsubscribe.push(subscr);
  }

  // public methods
  login(userName: string, password: string): Observable<UserType> {
    this.isLoadingSubject.next(true);

    return this.http.post<AuthModel>(`${this.baseUrl}Accounts/login`, { UserName: userName, Password: password })
      .pipe(map(auth => {
        const result = this.setAuthFromLocalStorage(auth);
        return result;
      }),
        switchMap(() => this.getUserByToken()),
        catchError((err) => {
          console.error('err', err);
          return of(undefined);
        }),
        finalize(() => this.isLoadingSubject.next(false)));
  }

  logout() {
    localStorage.removeItem(this.authLocalStorageToken);

    this.currentUserSubject.next(undefined);

    this.router.navigate(['/auth/login'], {
      queryParams: {},
    });
  }

  getUserByToken(): Observable<UserType> {
    const auth = this.getAuthFromLocalStorage();
    if (!auth || !auth.token) {
      return of(undefined);
    }

    if (auth) {
      this.currentUserSubject.next(auth);
    } else {
      this.logout();
    }
    return of(auth);
  }

  // need create new user then login
  registration(user: UserModel): Observable<any> {
    this.isLoadingSubject.next(true);


    return this.http.post(`${this.baseUrl}accounts/register`, user).pipe(
      map(() => {
        this.isLoadingSubject.next(false);
      }),
      switchMap(() => this.login(user.userName, user.password)),
      catchError((err) => {
        console.error('err', err);
        return of(undefined);
      }),
      finalize(() => this.isLoadingSubject.next(false))
    );

  }

  forgotPassword(email: string): Observable<boolean> {
    this.isLoadingSubject.next(true);
    return this.authHttpService
      .forgotPassword(email)
      .pipe(finalize(() => this.isLoadingSubject.next(false)));
  }

  // private methods
  private setAuthFromLocalStorage(auth: AuthModel): boolean {
    // store auth authToken/refreshToken/epiresIn in local storage to keep user logged in between page refreshes
    if (auth && auth.token) {
      localStorage.setItem(this.authLocalStorageToken, JSON.stringify(auth));
      return true;
    }
    return false;
  }

  private getAuthFromLocalStorage(): AuthModel | undefined {
    try {
      const lsValue = localStorage.getItem(this.authLocalStorageToken);
      if (!lsValue) {
        return undefined;
      }

      const authData = JSON.parse(lsValue);
      return authData;
    } catch (error) {
      console.error(error);
      return undefined;
    }
  }

  getAuthUser() {
    return this.http.get<AuthModel>(`${this.baseUrl}user`);
  }
  get UserFromLocalStorage(): AuthModel | undefined { return this.getAuthFromLocalStorage(); }

 
  updateAuthUser(user: UserType) { 

    return this.http.put<Result<AuthModel>>(`${this.baseUrl}user`, userToFormData(user as AuthModel))
      .pipe(map(res => {
        debugger
        const result = this.setAuthFromLocalStorage(res.data);
        return res.data;
      }));
  }

  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }
}
