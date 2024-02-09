import { AuthModel } from './auth.model'; 

export class UserModel extends AuthModel {
  id: number; 
  pic: string;
  roles: number[] = []; 
  

  setUser(_user: unknown) {
    const user = _user as UserModel;
   
    this.userName = user.userName || '';
    this.password = user.password || '';
    this.firstName = user.firstName || '';
    this.lastName = user.lastName || '';
    this.email = user.email || '';
    // this.pic = user.pic || './assets/media/avatars/blank.png';
    // this.roles = user.roles || [];
 
    this.phoneNumber = user.phoneNumber || '';
    this.address = user.address; 
  }
}
