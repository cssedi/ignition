import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmojiService } from '../../services/emoji.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ChallengeService } from '../../services/challenge.service';
import { DepartementChallengeService } from '../../services/departement-challenge.service';

@Component({
  selector: 'app-complete-challenge',
  templateUrl: './complete-challenge.component.html',
  styleUrls: ['./complete-challenge.component.scss']
})
export class CompleteChallengeComponent implements OnInit {
  challengeId! : number 
  selectedImage: string | ArrayBuffer | null | undefined;
  base64Image!: string ;
  modalVisible = false;
  showModal = false;
  name: string = '';
  emojis: any[] =[]
  isSubmit : boolean = false
  myForm: FormGroup;
  constructor(private formBuilder: FormBuilder, private challengeService : DepartementChallengeService, private route: ActivatedRoute, private router: Router,  ){
    this.myForm = this.formBuilder.group({
      myInput: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.challengeId =+ this.route.snapshot.paramMap.get('challengeId')!;
  
    
  }


  createEmoji(){
    var img = this.base64Image.split(',')[1]
    var sub = { 
      challengeId : this.challengeId,
      file : this.base64Image
    }
   this.challengeService.subminChallenge(sub).subscribe({
    next: (value) => {
      console.log(value)
    },complete: () => {
      this.isSubmit = true
    }, 
     error : (err) => {
      console.log(err)
    },
   })
    console.log(sub)
  }

   

  



  onImageDrop(event: DragEvent) {
    event.preventDefault();
    this.processImage(event.dataTransfer?.files!);
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
  }

  onFileSelected(event: Event) {
    const files = (event.target as HTMLInputElement).files;
    this.processImage(files);
  }
 
  processImage(files: FileList | null) {
    if (files && files.length > 0) {
      const reader = new FileReader();
      reader.onload = () => {
        this.selectedImage = reader.result;
        this.base64Image = reader.result as string;
      };
      reader.readAsDataURL(files[0]);
    }
  }
  clearImageUpload(){
     this.base64Image = ''
  }
  toggleModal() {
    this.showModal = !this.showModal;
    this.modalVisible = !this.modalVisible;
  }
  
  

  handleFileInput(event: any): void {
    const file = event.target.files[0];
    const reader = new FileReader();

    reader.onload = () => {
      this.base64Image = reader.result as string;
      console.log(this.base64Image)
    };

    reader.readAsDataURL(file);
  }
}
