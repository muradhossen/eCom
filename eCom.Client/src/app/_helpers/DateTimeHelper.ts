export function ConvertStringToFormatedDate(date : string){

    if (!date) {
        return "";
    }

    const dob = new Date(date);
    const yyyy = dob.getFullYear();
    const mm = String(dob.getMonth() + 1).padStart(2, '0');
    const dd = String(dob.getDate()).padStart(2, '0');
    const formatted = `${yyyy}-${mm}-${dd}`;

    return formatted;
}

export function ConvertDateToFormatedDate(date : Date){

    if (!date) {
        return "";
    } 
    
    const dob = new Date(date);
    const yyyy = dob.getFullYear();
    const mm = String(dob.getMonth() + 1).padStart(2, '0');
    const dd = String(dob.getDate()).padStart(2, '0');
    const formatted = `${yyyy}-${mm}-${dd}`;

    return formatted;
}