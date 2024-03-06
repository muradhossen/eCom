import { AuthModel } from "../modules/auth/models/auth.model";

export function userToFormData( user : AuthModel){

    const formData = new FormData();
    formData.append('firstName', user.firstName);
    formData.append('lastName', user.lastName);
    formData.append('dateOfBirth', user.dateOfBirth.toString());
    formData.append('gender', user.gender);
    formData.append('address', user.address);
    formData.append('email', user.email);
    formData.append('phoneNumber', user.phoneNumber);
    formData.append('email', user.email);
    formData.append('photo', user.photo);

    return formData;
}