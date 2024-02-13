import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, Subscription } from 'rxjs';
import { ConvertDateToFormatedDate } from 'src/app/_helpers/DateTimeHelper';
import { AuthService } from 'src/app/modules/auth';
import { AuthModel } from 'src/app/modules/auth/models/auth.model';

@Component({
  selector: 'app-profile-details',
  templateUrl: './profile-details.component.html',
})
export class ProfileDetailsComponent implements OnInit, OnDestroy {
  isLoading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isLoading: boolean;
  private unsubscribe: Subscription[] = [];
  user: AuthModel = new AuthModel();
  userForm: FormGroup;

  constructor(private cdr: ChangeDetectorRef,
    private authService: AuthService,
    private fb: FormBuilder) {
    const loadingSubscr = this.isLoading$
      .asObservable()
      .subscribe((res) => (this.isLoading = res));
    this.unsubscribe.push(loadingSubscr);
  }

  ngOnInit(): void {
    this.initForm();

    this.authService.getAuthUser()
      .subscribe(res => {
        this.user = res;
        this.assignValueToForm();
      });



  }

  saveSettings() {
    this.isLoading$.next(true);
    setTimeout(() => {
      this.isLoading$.next(false);
      this.cdr.detectChanges();
    }, 1500);
  }

  initForm() {
    this.userForm = this.fb.group({
      firstName: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(320),
        ]),
      ],
      lastName: [
        '',
        Validators.compose([
          Validators.minLength(3),
          Validators.maxLength(100),
        ]),
      ],
      dateOfBirth: [new Date()],
      gender: [''],
      address: [''],
      userName: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(55),
        ]),
      ],
      email: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(320),
        ]),
      ],
      phoneNumber: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(11),
          Validators.maxLength(11),
        ]),
      ],
    });
  }

  assignValueToForm() { 

    this.userForm.get('firstName')?.setValue(this.user.firstName);
    this.userForm.get('lastName')?.setValue(this.user.lastName);
    if (this.user.dateOfBirth) { 
      const todayFormatted = ConvertDateToFormatedDate(this.user.dateOfBirth); 
      this.userForm.get('dateOfBirth')?.setValue(todayFormatted);
    }


    this.userForm.get('gender')?.setValue(this.user.gender);
    this.userForm.get('address')?.setValue(this.user.address);
    this.userForm.get('userName')?.setValue(this.user.userName);
    this.userForm.get('email')?.setValue(this.user.email);
    this.userForm.get('phoneNumber')?.setValue(this.user.phoneNumber);

  }

  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }
}
