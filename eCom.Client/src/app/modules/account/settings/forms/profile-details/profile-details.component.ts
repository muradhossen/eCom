import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, Subscription } from 'rxjs';
import { ConvertDateToFormatedDate } from 'src/app/_helpers/DateTimeHelper';
import { AuthService } from 'src/app/modules/auth';
import { AuthModel } from 'src/app/modules/auth/models/auth.model';

@Component({
  selector: 'app-profile-details',
  templateUrl: './profile-details.component.html',
  styleUrls: ['./profile-details.component.css']
})
export class ProfileDetailsComponent implements OnInit, OnDestroy {
  isLoading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isLoading: boolean;
  private unsubscribe: Subscription[] = [];
  user: AuthModel = new AuthModel();
  userForm: FormGroup;
  userPhoto: File;

  userProfilePhotoUrl = "./assets/media/avatars/blank.png"//"./assets/media/avatars/300-1.jpg"

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
 
    this.initForm();

    this.authService.getAuthUser()
      .subscribe(res => {
        this.setUser(res);
      }); 
  }

  setUser(user : AuthModel){
    this.user = user;
    if (this.user.photoUrl) {
      this.userProfilePhotoUrl = this.user.photoUrl;
    }
    this.cdr.detectChanges()
    this.assignValueToForm();
  }
  // Method to handle file selection
  onFileSelected(event: any) {
    debugger
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.userPhoto = file;
    }
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
    user.photo = this.userPhoto;

    this.authService.updateAuthUser(user).subscribe(
      {
        next: (res) => {
         
          this.setUser(res);
         },
        error: error => this.isLoading$.next(false),
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
      photo: ['']
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
