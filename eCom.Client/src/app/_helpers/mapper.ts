import { Category } from "../modules/apps/models/category";
import { Product } from "../modules/apps/models/product";
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

export function productToFormData(product: Product) {

    const formData = new FormData();
    formData.append('name', product.name);
    formData.append('subcategoryId',product.subCategoryId.toString());
    
    if (product.description) {
        formData.append('description', product.description);
    }
    if (product.details) {
        formData.append('details', product.details);
    }
    if (product.usp) {
        formData.append('usp', product.usp);
    }
    if (product.code) {
        formData.append('code', product.code);
    }
    if (product.image) {
        formData.append('image', product.image);
    }
    if (product.imageUrl) {
        formData.append('ImageUrl', product.imageUrl);
    } 

        // Append section data
        if (product.section) {
            formData.append('section[name]', product.section.name);
    
            // Append pricing items data
            if (product.section.pricingItems && product.section.pricingItems.length > 0) {
                product.section.pricingItems.forEach((item, index) => {
                    formData.append(`section[pricingItems][${index}][price]`, item.price.toString());
                    formData.append(`section[pricingItems][${index}][label]`, item.label);
                });
            }
        }
    return formData;
}