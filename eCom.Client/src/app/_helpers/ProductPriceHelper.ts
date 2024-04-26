import { DiscountType } from "../modules/apps/enums/discountType";
import { PricingItem } from "../modules/apps/models/product";

export function getProductPrice(pricingItem: PricingItem): number {
    if (hasDiscount(pricingItem)) {
        return pricingItem.price - getDiscountAmount(pricingItem);
    } else {
        return pricingItem.price;
    }
}

export function hasDiscount(pricingItem: PricingItem) {
    return pricingItem.discountAmount || pricingItem.discountPercentage;
}

function getDiscountAmount(pricingItem: PricingItem) {

    if (pricingItem.discountType == DiscountType.flat) {
        return pricingItem.discountAmount;
    }
    if (pricingItem.discountType == DiscountType.percentage) {
        return (pricingItem.price * pricingItem.discountPercentage) / 100;
    }
    return 0;
}

