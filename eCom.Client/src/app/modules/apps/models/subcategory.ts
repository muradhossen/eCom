import { SlNumber } from "./sl-number";

export class SubCategory implements SlNumber {
    sl: number; 
    id: number;
    code: string;
    name: string;
    imageUrl: string;
    description: string;
    image : File;
    categoryId : number;
}

