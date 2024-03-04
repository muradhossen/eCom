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
      .subscribe((res) => {
     
        this.isLoading = res
       
      });
    this.unsubscribe.push(loadingSubscr);
    
  }

  ngOnInit(): void {

    //  this.initFormV2();
    this.initForm();

    this.authService.getAuthUser()
      .subscribe(res => {
        this.user = res;
        this.assignValueToForm();
      });



  }

  saveSettings() {
    this.isLoading = true;

    const user = new AuthModel();
    user.firstName = this.userForm.get('firstName')?.value;
    user.lastName = this.userForm.get('lastName')?.value;
    user.dateOfBirth = this.userForm.get('dateOfBirth')?.value;
    user.gender = this.userForm.get('gender')?.value;
    user.address = this.userForm.get('address')?.value;
    user.email = this.userForm.get('email')?.value;
    user.phoneNumber = this.userForm.get('phoneNumber')?.value;

    this.authService.updateAuthUser(user).subscribe(
      {
        next: (res) => { 
        }, 
        complete: () => {
          this.isLoading$.next(false);
          this.cdr.detectChanges();
        }
      });

  }

  initForm() {
    this.userForm = this.fb.group({
      firstName: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(10),
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
          Validators.email,
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
    this.userForm.patchValue({
      firstName: this.user.firstName,
      lastName: this.user.lastName,
      userName: this.user.userName,
      dateOfBirth: ConvertDateToFormatedDate(this.user.dateOfBirth),
      gender: this.user.gender,
      address: this.user.address,
      email: this.user.email,
      phoneNumber: this.user.phoneNumber
    });

  }

  ngOnDestroy() {
    this.userForm.reset();

    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }
}
