import { IUserLogin } from './user-login';

export interface IUserSignUp extends IUserLogin {
  fullname: string;
  repeatPass: string;
}