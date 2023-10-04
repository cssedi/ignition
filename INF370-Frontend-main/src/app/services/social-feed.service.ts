import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PostDto } from '../Models/post-dto';
import { Observable } from 'rxjs';
import { CommentDto } from '../Models/comment-dto';
import { PostComment } from '../Models/comment';


@Injectable({
  providedIn: 'root'
})
export class SocialFeedService {
  token = localStorage.getItem('token')

  constructor(private http : HttpClient) { }

  httpOptions = {
    headers : new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${this.token}`
      })
    };

  Post(post : PostDto ): Observable<any>{
 
   
    post.likes = 0;
  
    // Make the HTTP POST request with the token in the headers
    return this.http.post<any>('https://localhost:7269/api/Post/AddPostAuth', post, this.httpOptions);
  }

  GetAllPosts():Observable<any> {
    return this.http.get<any>('https://localhost:7269/api/Post/GetAllPosts', this.httpOptions);

  }

  GetComments(id:number):Observable<PostComment[]>{
    return this.http.get<PostComment[]>('https://localhost:7269/api/Comment/GetComments/'+id, this.httpOptions);

  }
  

  likePost(postId : number ) {
    return this.http.get<any>('https://localhost:7269/api/Post/LikePost/'+postId, this.httpOptions);

  }
  createComment( comment : CommentDto) : Observable<any> {
    return this.http.post<any>('https://localhost:7269/api/Comment/AddComment',  comment, this.httpOptions);

  }
  viewOtherUser(challengerId : string ) : Observable<any> { 
    return this.http.get<any>('https://localhost:7269/api/Auth/GetUserById/' + challengerId, this.httpOptions);
  }

  deleteComment(commentId : number) : Observable<any> {
    return this.http.delete<any>(  'https://localhost:7269/api/Comment/DeleteComment/' + commentId,this.httpOptions)
  }

  getPostsReport() : Observable<any> {
    return this.http.get<any>('https://localhost:7269/api/Post/PostsReport', this.httpOptions);
  }

  deletePost(postId : number) : Observable<any> {
    return this.http.delete<any>(  'https://localhost:7269/api/Post/DeletePost/' + postId,this.httpOptions)
  }

}
