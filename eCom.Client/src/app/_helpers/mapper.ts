import { Category } from "../modules/apps/models/category";
import { SubCategory } from "../modules/apps/models/subcategory";
import { AuthModel } from "../modules/auth/models/auth.model";

export function userToFormData(user: AuthModel) {

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


export function categoryToFormData(category: Category) {

    const formData = new FormData();
    formData.append('name', category.name);
    if (category.description) {
        formData.append('description', category.description);
    }
    if (category.code) {
        formData.append('code', category.code);
    }
    if (category.image) {
        formData.append('image', category.image);
    }
    if (category.imageUrl) {
        formData.append('ImageUrl', category.imageUrl);
    }
    return formData;
}


export function subCategoryToFormData(subCategory: SubCategory) {

    const formData = new FormData();
    formData.append('name', subCategory.name);
    formData.append('categoryId',subCategory.categoryId.toString());
    
    if (subCategory.description) {
        formData.append('description', subCategory.description);
    }
    if (subCategory.code) {
        formData.append('code', subCategory.code);
    }
    if (subCategory.image) {
        formData.append('image', subCategory.image);
    }
    if (subCategory.imageUrl) {
        formData.append('ImageUrl', subCategory.imageUrl);
    }
    return formData;
}