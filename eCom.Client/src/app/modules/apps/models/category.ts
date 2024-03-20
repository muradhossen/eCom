import { SlNumber } from "./sl-number";

export class Category implements SlNumber {
    sl: number; 
    id: number;
    code: string;
    name: string;
    imageUrl: string;
    description: string;
    image : File;
}

