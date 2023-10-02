export interface Prize {
    prizeID:number,
name : string,
description:String,
frontImgURL:string,
backImgURL: string, 
price: number
//relationships
prizeTypeID: number
}
