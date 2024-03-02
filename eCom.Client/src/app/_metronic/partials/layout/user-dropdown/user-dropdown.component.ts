import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/modules/auth';

@Component({
  selector: 'app-user-dropdown',
  templateUrl: './user-dropdown.component.html',
  styleUrls: ['./user-dropdown.component.css']
})
export class UserDropdownComponent implements OnInit {
  @Input() itemClass : string;
  userAvatarClass: string = 'symbol-35px symbol-md-40px';
  constructor(private authService : AuthService, private router : Router) { }

  ngOnInit() {
  }

  logOut(){ 
    this.authService.logout();  
  }

}
