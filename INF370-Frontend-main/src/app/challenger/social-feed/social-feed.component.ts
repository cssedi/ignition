import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component } from '@angular/core';
import { SocialFeedService } from '../../services/social-feed.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { PostDto } from '../../Models/post-dto';
import { CommentDto } from 'src/app/Models/comment-dto';
import { Dropdown } from "flowbite";
import type { DropdownOptions, DropdownInterface } from "flowbite";
import { NgToastService } from 'ng-angular-popup';
import { Router } from '@angular/router';
import { OpenAIService } from 'src/app/services/OpenAI/open-ai.service';
import { PostComment } from 'src/app/Models/comment';

@Component({
  selector: 'app-social-feed',
  templateUrl: './social-feed.component.html',
  styleUrls: ['./social-feed.component.scss']
})
export class SocialFeedComponent implements AfterViewInit {
  // Variables 
  postForm!:FormGroup;
  formSubmitted: boolean = false
  toggleCommentDropDown: boolean = false
  togglePostDropDown : boolean = false
  showSection: boolean = false;
  posts : any[] = []
  comments : PostComment[] = []
  comment : string = '' 
  searchTerm!: string
  //Open AI variables
  sentimentResult = '';
  user = JSON.parse(localStorage.getItem('user')!)
  // Constructor 
  constructor(private socialFeedService : SocialFeedService,private fb: FormBuilder, private toast: NgToastService, private router : Router, private openAIService: OpenAIService ){
    this.postForm = this.fb.group({

      text : new FormControl('', Validators.required ),
      
    })
   
    this.getUserPosts()
  
  }
  signOut(){
    localStorage.removeItem('token')
    localStorage.removeItem('user')
    this.router.navigate([''])

  }
  ngAfterViewInit() : void {
   
    
  }

  deletePost(postId : number){
    this.socialFeedService.deletePost(postId).subscribe({
      next: (value) => {
        console.log('on delete post', value)
      },
      complete: () => {
        this.getUserPosts()
      }
    })
  }



  searchAudit() {
    console.log(this.posts)

    console.log('searchAudit called with searchTerm:', this.searchTerm);
    this.posts = this.posts.filter(posts =>
      posts.text.toLowerCase().includes(this.searchTerm.toLowerCase())||
      posts.challenger.userName.toLowerCase().includes(this.searchTerm.toLowerCase())||
       posts.challenger.surname.toLowerCase().includes(this.searchTerm.toLowerCase())||
      posts.text.toLowerCase().includes(this.searchTerm.toLowerCase())||

      this.formatTimestamp(posts.date).includes(this.searchTerm.toLowerCase())

    );

    if (this.searchTerm == '') {
      this.searchTerm = ''
      this.getUserPosts()
    }

  }
  clearSearch() {
    this.searchTerm = ''
    this.getUserPosts();
  }

  formatTimestamp(timestamp: string | number | Date) {
    const date = new Date(timestamp);
    const day = date.getDate().toString().padStart(2, '0');
    const monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    const month = monthNames[date.getMonth()];
    const year = date.getFullYear();
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    return `${day} ${month} ${year} ${hours}:${minutes}`;
  }

  viewProfile( challengerId : string ){
    this.socialFeedService.viewOtherUser(challengerId).subscribe({
      next: (value) => {
        console.log('on view profile', value)
      }, 
      error : (error) => {
      console.log('error on like', error.error)
      }
    })
    this.router.navigate(['/profile/'])
  }
  likePost(PostId : number){
    this.socialFeedService.likePost(PostId).subscribe({ 
      next: (value) => {
        console.log('on like', value)
      }, 
      complete:() => {
        this.getUserPosts()
      },
      error : (error) => {
      console.log('error on like', error.error)
      }
    })
  }
  getUserPosts() {


    this.socialFeedService.GetAllPosts().subscribe({
      next: (posts : any[]) => {
        this.posts = posts.slice().reverse()
        console.log(this.posts)
    
        console.log('user posts', this.posts)
      }, error: (error) => {

      }
      
    })
  }
  // Methods
  showCommentPostId : number = 0;
  toggleSection(posId : number) {
    this.showCommentPostId = posId
   this.showSection = !this.showSection;
   if(this.showSection == false){
    this.showCommentPostId = 0
   }
   if(this.showSection == true){
    this.showCommentPostId = posId 
   }

   this.socialFeedService.GetComments(posId)
   .subscribe({
    next:(response)=>{
      this.comments = response;
      console.log(response)
    }
    ,complete: () => {
      
    }
   })
  }
  showButtonCommentId : number = 0;
  toggleCommentOptions(commentId : number) {
    this.showButtonCommentId = commentId
    console.log("Button clicked");
    this.toggleCommentDropDown = !this.toggleCommentDropDown;
    console.log(this.toggleCommentDropDown);
  }
  showButtonPostId:number =0
  togglePostOptions(PostId : number) {
    this.showButtonPostId = PostId
    this.togglePostDropDown = !this.togglePostDropDown
  }

  Post(){
    let postDto : PostDto = {
      text: this.postForm.value.text,
      likes : 110
    }
    this.formSubmitted = true
    if(this.postForm.valid){
      // check text
      this.openAIService.analyzeText(this.postForm.value.text.toLowerCase())
      .subscribe(containsSwearWords => {
        if (containsSwearWords['sentiment'] == 'negative' || containsSwearWords['containsSwearWords'] == true) {
          this.toast.warning({
            detail: "WARNING",
            summary: "This post seems to contain unpleasant language, be mindful of what you post as this is a shared platform",
            duration: 5000
          });
        } else {
          // Proceed with post creation logic
          this.socialFeedService.Post(postDto).subscribe({
            next: (response) => {
            },
            complete: () => {
              this.getUserPosts();
              window.location.reload();
            }
          });
        }
      })

    }
    else{
      this.toast.error({detail:"ERROR", summary: "Post text can not be empty!", duration: 5000})
    }

  }
  
  deleteComment(commentId : number) {
    this.socialFeedService.deleteComment(commentId).subscribe({
      next: (value) => {
        console.log('on delete comment', value)
      },
      complete: () => {
        this.getUserPosts()
        window.location.reload();
      }
    })
  }
  createComment(postId : number){
    console.log('comment text', this.comment)
    var comment : CommentDto = {
      postId : postId,
      CommentText : this.comment
    }
    this.comment =''
    this.openAIService.analyzeText(comment.CommentText.toLowerCase())
    .subscribe(containsSwearWords => {
      if (containsSwearWords['sentiment'] == 'negative' || containsSwearWords['containsSwearWords'] == true) {
        this.toast.warning({
          detail: "WARNING",
          summary: "This post seems to contain some unpleasant language, please be mindful of the things you say to others!",
          duration: 5000
        });
      } else {
        // Proceed with comment creation logic
        this.socialFeedService.createComment(comment).subscribe({
          next : (response) => {
            console.log('on create commment', response)
          }, complete : () => {
            this.getUserPosts()
          }, error:(err) => {
            console.log(err)
          },
        })
      }
    })
    
  }

}
