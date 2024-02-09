export class AuthModel {
  authToken: string;
  refreshToken: string;
  expiresIn: Date;
 
    userName: string;
    token: string;
    photoUrl: string;
    gender: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    dateOfBirth: string;
    address: string;
    password : string;
 

  setAuth(auth: AuthModel) {
    this.authToken = auth.authToken;
    this.refreshToken = auth.refreshToken;
    this.expiresIn = auth.expiresIn;
  }
}
