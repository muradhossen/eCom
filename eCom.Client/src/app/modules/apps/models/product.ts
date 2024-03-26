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
    section : SectionCreate;
}


export class SectionCreate {
    public name: string;
    public pricingItems: PricingItemCreate[];
}

export class PricingItemCreate {
    public price: number;
    public label: string;
}

