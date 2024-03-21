export class Result<T> {

public isSuccess : boolean;
public errorMessage : string;
public error : Error;
public data : T;
}


    
export class Error {
    public Code : string;
    public Description : string;
}