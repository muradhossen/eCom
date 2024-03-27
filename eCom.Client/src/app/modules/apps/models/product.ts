import { SlNumber } from "./sl-number";

export class Product implements SlNumber {
    sl: number;
    id: number;
    code: string;
    name: string;
    imageUrl: string;
    description: string;
    details: string;
    usp: string;
    image: File;
    subCategoryId: number;
    section : Section = new Section();
}


export class Section {
    public name: string;
    public pricingItems: PricingItem[] = [];
}

export class PricingItem {
    public price: number;
    public label: string;
}

