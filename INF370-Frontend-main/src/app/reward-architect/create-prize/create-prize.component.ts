import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Prize } from 'src/app/Models/prize';
import { PrizeDto } from 'src/app/Models/prize-dto';
import { ShopService } from 'src/app/services/shop.service';

@Component({
  selector: 'app-create-prize',
  templateUrl: './create-prize.component.html',
  styleUrls: ['./create-prize.component.scss']
})
export class CreatePrizeComponent {
  selectedImage: string | ArrayBuffer | null | undefined;
  base64Image!: string ;
  productForm!: FormGroup ;
  prizeTypes : any[] = []
  constructor(private fb: FormBuilder, private shopService : ShopService, private toast:NgToastService, private Router:Router) { }

  ngOnInit(): void {
    this.shopService.getAllPrizeTypes().subscribe({
      next:(value)  => {
        this.prizeTypes = value
        console.log('prize types', this.prizeTypes)
      },
    })

    this.productForm = this.fb.group({
      productName: ['', [Validators.required]],
      tokens: [null, [Validators.required, Validators.min(0)]],
      prizeType: ['', [Validators.required]],
      description: ['', [Validators.required]],
  
    });
  }
  
  onSubmit(){
    
   var  prize : PrizeDto = {
   name :  this.productForm.value.productName,
   tokens : this.productForm.value.tokens ,
   Image : this.base64Image, 
   description : this.productForm.value.description,
   prizeTypeId : this.productForm.value.prizeType

   }
    console.log(this.productForm.value)
   if(this.productForm.valid){
      this.shopService.createPrize(prize).subscribe({
        next:(value) => {
          console.log('create prize ', value)
          this.Router.navigate(['/view-rewards'])
          this.toast.success({detail:"SUCCESS", summary:'Reward created successfully' , duration:5000})
        },error:(err) => {
          console.log('create prize error ', err.error)
        },
      })
   }

  }

  // Image processing 
  clearImageUpload(){
    this.base64Image = ''
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
  handleFileInput(event: any): void {
    const file = event.target.files[0];
  
    if (!file) {
      // No file selected, you can show an error message or handle it as needed
      console.log('No file selected');
      return;
    }
  
    // File type validation
    const allowedFileTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/svg+xml'];
    if (!allowedFileTypes.includes(file.type)) {
      // Invalid file type, show an error message to the user
      this.toast.error({detail:"ERROR", summary:'Invalid file type. Please upload an image (JPEG, PNG, GIF, or SVG).' , duration:5000})
      return;
    }
  
    // File size validation (example: 2 MB limit)
    const maxSizeInBytes = 2 * 1024 * 1024; // 2 MB in bytes
    if (file.size > maxSizeInBytes) {
      // File size exceeds the limit, show an error message to the user
      this.toast.error({detail:"ERROR", summary:'File size exceeds the allowed limit (2 MB).' , duration:5000})
      return;
    }
  
    const reader = new FileReader();
  
    reader.onload = () => {
      this.base64Image = reader.result as string;
      console.log(this.base64Image);
    };
  
    reader.readAsDataURL(file);
  }

  onImageDrop(event: DragEvent) {
    event.preventDefault();
    this.processImage(event.dataTransfer?.files!);
  }
}
