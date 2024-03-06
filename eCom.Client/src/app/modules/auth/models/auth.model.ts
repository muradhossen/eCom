export class AuthModel {
  authToken: string;
  refreshToken: string;
  expiresIn: Date;

  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  gender: string;
  address: string;
  lastActive: Date;
  created: Date;
  userName: string;
  email: string;
  token: string;
  phoneNumber: string;
  password: string;
  photoUrl: string;
  photo : File;

  setAuth(auth: AuthModel) {
    this.authToken = auth.authToken;
    this.refreshToken = auth.refreshToken;
    this.expiresIn = auth.expiresIn;
  }
}
