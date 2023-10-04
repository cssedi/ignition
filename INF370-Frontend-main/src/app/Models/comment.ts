import { RegisterModel } from "./register-model";

export interface PostComment{
    commentID: number;
    text: string;
    postID: number;
    challengerId: string; 
    challenger: RegisterModel; // Optional navigation property
}